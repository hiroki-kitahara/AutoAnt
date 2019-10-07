using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.Framework;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// 道路のモデルを切り替えるクラス
    /// </summary>
    public sealed class RoadModelSwitcher : MonoBehaviour, ICellEventGimmick
    {
        private static class Direction
        {
            public const int Up    = (1 << 0);
            public const int Down  = (1 << 1);
            public const int Left  = (1 << 2);
            public const int Right = (1 << 3);
        }

        /// <summary>
        /// 直進モデル
        /// </summary>
        [SerializeField]
        private PoolableObject straight = null;

        /// <summary>
        /// 90度に曲がる道路モデル
        /// </summary>
        [SerializeField]
        private PoolableObject turn = null;

        /// <summary>
        /// T字路モデル
        /// </summary>
        [SerializeField]
        private PoolableObject junctionT = null;

        /// <summary>
        /// 十字路モデル
        /// </summary>
        [SerializeField]
        private PoolableObject crossroads = null;

        private ModelMapper modelMapper;

        private PoolableObject currentPrefab;

        private PoolableObject currentModel;

        private ObjectPool<PoolableObject> currentPool;

        private static readonly ObjectPoolBundle<PoolableObject> pools = new ObjectPoolBundle<PoolableObject>();

        public void Attach(CellEvent cellEvent)
        {
            Assert.IsNotNull(cellEvent);
            
            this.modelMapper = new ModelMapper(
                this.straight,
                this.turn,
                this.junctionT,
                this.crossroads
            );

            this.UpdateModel(cellEvent);

            cellEvent.Broker.Receive<RequestUpdateRoadModel>()
                .SubscribeWithState2(this, cellEvent, (_, _this, _cellEvent) =>
                {
                    _this.UpdateModel(_cellEvent);
                })
                .AddTo(this);

            this.PublishRequestUpdateRoadModelCross(cellEvent);
        }

        public void Detach(CellEvent cellEvent)
        {
            this.PublishRequestUpdateRoadModelCross(cellEvent);
            this.ReturnToPoolModel();
        }

        /// <summary>
        /// 周囲の道路状況から自分自身の道路モデルを設定する
        /// </summary>
        /// <param name="cellEvent"></param>
        private void UpdateModel(CellEvent cellEvent)
        {
            var cellMapper = GameSystem.Instance.CellManager.Mapper;
            var bit = 0;
            bit |= this.ExistsRoad(cellMapper, cellEvent, Vector2Int.up) ? Direction.Up : 0;
            bit |= this.ExistsRoad(cellMapper, cellEvent, Vector2Int.down) ? Direction.Down : 0;
            bit |= this.ExistsRoad(cellMapper, cellEvent, Vector2Int.left) ? Direction.Left : 0;
            bit |= this.ExistsRoad(cellMapper, cellEvent, Vector2Int.right) ? Direction.Right : 0;
            Assert.IsTrue(this.modelMapper.Map.ContainsKey(bit), $"Origin = {cellEvent.Origin}, bit = {bit}の道路マップがありませんでした");
            var modelParameter = this.modelMapper.Map[bit];

            // モデルが違った場合のみ切り替える
            if(modelParameter.ModelPrefab != this.currentPrefab)
            {
                this.ReturnToPoolModel();
                this.currentPool = pools.Get(modelParameter.ModelPrefab);
                this.currentModel = this.currentPool.Rent();
                this.currentModel.transform.SetParent(this.transform);
                this.currentModel.transform.localPosition = Vector3.zero;
                this.currentPrefab = modelParameter.ModelPrefab;
            }

            this.currentModel.transform.localEulerAngles = Vector3.up * modelParameter.Rotation;
        }

        /// <summary>
        /// モデルを解放する
        /// </summary>
        private void ReturnToPoolModel()
        {
            if(this.currentModel == null)
            {
                return;
            }

            this.currentPool.Return(this.currentModel);
            this.currentModel.transform.SetParent(null);
        }

        /// <summary>
        /// <paramref name="direction"/>方向にある道路のモデル設定をリクエストする
        /// </summary>
        private void PublishRequestUpdateRoadModel(CellEvent owner, Vector2Int direction)
        {
            var cellEventMap = GameSystem.Instance.CellManager.Mapper.CellEvent.Map;
            var position = owner.Origin + direction;
            if(!cellEventMap.ContainsKey(position))
            {
                return;
            }

            cellEventMap[position].Broker.Publish(RequestUpdateRoadModel.Get());
        }

        /// <summary>
        /// 上下左右にある道路のモデル設定をリクエストする
        /// </summary>
        private void PublishRequestUpdateRoadModelCross(CellEvent cellEvent)
        {
            this.PublishRequestUpdateRoadModel(cellEvent, Vector2Int.up);
            this.PublishRequestUpdateRoadModel(cellEvent, Vector2Int.down);
            this.PublishRequestUpdateRoadModel(cellEvent, Vector2Int.left);
            this.PublishRequestUpdateRoadModel(cellEvent, Vector2Int.right);
        }

        /// <summary>
        /// <paramref name="direction"/>方向に道路があるか返す
        /// </summary>
        private bool ExistsRoad(CellMapper mapper, CellEvent owner, Vector2Int direction)
        {
            var position = owner.Origin + direction;
            if(!mapper.CellEvent.Map.ContainsKey(position))
            {
                return false;
            }

            return mapper.CellEvent.Map[position] is IRoad;
        }

        /// <summary>
        /// 各状況に応じた道路モデルのマッピングを担うクラス
        /// </summary>
        public class ModelMapper
        {
            public readonly Dictionary<int, ModelParameter> Map = new Dictionary<int, ModelParameter>();

            public ModelMapper(
                PoolableObject straight,
                PoolableObject turn,
                PoolableObject junctionT,
                PoolableObject crossroads
            )
            {
                // 全ての状況を網羅してマッピング

                this.Map.Add(0, new ModelParameter(straight, 0.0f));

                this.Map.Add(Direction.Up, new ModelParameter(straight, 0.0f));
                this.Map.Add(Direction.Down, new ModelParameter(straight, 0.0f));
                this.Map.Add(Direction.Left, new ModelParameter(straight, 90.0f));
                this.Map.Add(Direction.Right, new ModelParameter(straight, 90.0f));

                this.Map.Add(Direction.Up | Direction.Down, new ModelParameter(straight, 0.0f));
                this.Map.Add(Direction.Left | Direction.Right, new ModelParameter(straight, 90.0f));

                this.Map.Add(Direction.Up | Direction.Left, new ModelParameter(turn, 0.0f));
                this.Map.Add(Direction.Up | Direction.Right, new ModelParameter(turn, 90.0f));
                this.Map.Add(Direction.Down | Direction.Right, new ModelParameter(turn, 180.0f));
                this.Map.Add(Direction.Down | Direction.Left, new ModelParameter(turn, 270.0f));

                this.Map.Add(Direction.Up | Direction.Down | Direction.Left, new ModelParameter(junctionT, 0.0f));
                this.Map.Add(Direction.Up | Direction.Left | Direction.Right, new ModelParameter(junctionT, 90.0f));
                this.Map.Add(Direction.Up | Direction.Down | Direction.Right, new ModelParameter(junctionT, 180.0f));
                this.Map.Add(Direction.Down | Direction.Left | Direction.Right, new ModelParameter(junctionT, 270.0f));

                this.Map.Add(Direction.Up | Direction.Down | Direction.Left | Direction.Right, new ModelParameter(crossroads, 0.0f));
            }
        }

        /// <summary>
        /// モデル生成に必要なパラメータ
        /// </summary>
        public class ModelParameter
        {
            /// <summary>
            /// モデルプレハブ
            /// </summary>
            public readonly PoolableObject ModelPrefab;

            /// <summary>
            /// モデルの回転値
            /// </summary>
            public readonly float Rotation;

            public ModelParameter(PoolableObject model, float rotate)
            {
                this.ModelPrefab = model;
                this.Rotation = rotate;
            }
        }
    }
}
