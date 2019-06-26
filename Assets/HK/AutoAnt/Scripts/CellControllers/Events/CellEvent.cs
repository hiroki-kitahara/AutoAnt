using System;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.EffectSystems;
using HK.Framework.EventSystems;
using HK.AutoAnt.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    public abstract class CellEvent : ScriptableObject, ICellEvent
    {
        [SerializeField]
        protected Constants.CellEventCategory category;
        public Constants.CellEventCategory Category => this.category;

        [SerializeField]
        protected CellEventGenerateCondition condition = null;

        [SerializeField]
        protected int size = 1;
        public int Size => this.size;

        [SerializeField]
        protected AudioClip constructionSE = null;

        [SerializeField]
        protected AudioClip destructionSE = null;

        [SerializeField]
        protected PoolableEffect constructionEffect = null;

        [SerializeField]
        protected PoolableEffect destructionEffect = null;

        public int Id => int.Parse(this.name);

        public Vector2Int Origin { get; protected set; }

        /// <summary>
        /// 実体が持つイベント
        /// </summary>
        protected readonly CompositeDisposable instanceEvents = new CompositeDisposable();

        protected CellGimmickController gimmick;

        public abstract CellGimmickController CreateGimmickController(Vector2Int origin);

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            this.size = this.size <= 1 ? 1 : this.size;
        }
#endif

        public virtual void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            this.Origin = position;

            // 自分自身のマスターデータを取得してデータを参照している
            // セーブデータから読み込む時にアセットの参照はセーブしていないのでちょっとややこしい作りになっている
            var record = gameSystem.MasterData.CellEvent.Records.Get(this.Id);
            this.gimmick = record.EventData.CreateGimmickController(this.Origin);

            if(!isInitializingGame)
            {
                Assert.IsNotNull(record.EventData.constructionSE, $"Id = {this.Id}の建設時のSE再生に失敗しました");
                AutoAntSystem.Audio.SE.Play(record.EventData.constructionSE);

                Assert.IsNotNull(record.EventData.constructionEffect, $"Id = {this.Id}の建設時のエフェクト生成に失敗しました");
                var effect = record.EventData.constructionEffect.Rent();
                effect.transform.position = this.gimmick.transform.position;
                effect.transform.localScale = Vector3.one * record.EventData.size;
            }

            Broker.Global.Publish(AddedCellEvent.Get(this));
        }

        public virtual void Remove(GameSystem gameSystem)
        {
            this.instanceEvents.Clear();

            // 自分自身のマスターデータを取得してデータを参照している
            // セーブデータから読み込む時にアセットの参照はセーブしていないのでちょっとややこしい作りになっている
            var record = gameSystem.MasterData.CellEvent.Records.Get(this.Id);

            Assert.IsNotNull(record.EventData.destructionSE, $"Id = {this.Id}の破壊時のSE再生に失敗しました");
            AutoAntSystem.Audio.SE.Play(record.EventData.destructionSE);

            Assert.IsNotNull(record.EventData.destructionEffect, $"Id = {this.Id}の破壊時のエフェクト生成に失敗しました");
            var effect = record.EventData.destructionEffect.Rent();
            effect.transform.position = this.gimmick.transform.position;
            effect.transform.localScale = Vector3.one * record.EventData.size;

            Broker.Global.Publish(RemovedCellEvent.Get(this));

            Destroy(this.gimmick.gameObject);
        }

        public bool CanGenerate(Cell origin, int cellEventRecordId, GameSystem gameSystem, CellMapper cellMapper)
        {
            Assert.IsNotNull(this.condition);

            var cellPositions = cellMapper.GetRange(origin.Position, Vector2Int.one * this.size, p => cellMapper.Cell.Map.ContainsKey(p));

            // 配置したいところにセルがない場合は生成できない
            if(cellPositions.Length != this.size * this.size)
            {
                return false;
            }

            // 配置したいセルにイベントがあった場合は生成できない
            var cells = cellMapper.GetCells(cellPositions);
            if(Array.FindIndex(cells, c => cellMapper.HasEvent(c)) != -1)
            {
                return false;
            }

            // コストが満たしていない場合は生成できない
            var masterData = gameSystem.MasterData.LevelUpCost.Records.Get(cellEventRecordId, 0);
            if(!masterData.Cost.IsEnough(gameSystem.User, gameSystem.MasterData.Item))
            {
                return false;
            }

            return this.condition.Evalute(cells);
        }

        public virtual void OnClick(Cell owner)
        {
        }

#if UNITY_EDITOR
        public static CellEvent GetOrCreateAsset(Database.SpreadSheetData.CellEventData data)
        {
            switch(data.Classname)
            {
                case "Housing":
                    return CellEvent.InternalGetOrCreateAsset<Housing>(data);
                case "Facility":
                    return CellEvent.InternalGetOrCreateAsset<Facility>(data);
                default:
                    Debug.LogError($"CellEventType = {data.Classname}は未対応の値です");
                    return null;
            }
        }

        protected static T InternalGetOrCreateAsset<T>(Database.SpreadSheetData.CellEventData data)
            where T : CellEvent
        {
            var path = $"Assets/HK/AutoAnt/DataSources/CellEvents/{data.Classname}/{data.Id}.asset";
            var result = AssetDatabase.LoadAssetAtPath<T>(path);
            if (result == null)
            {
                result = CreateInstance<T>();
                result.name = data.Id.ToString();
                AssetDatabase.CreateAsset(result, path);
            }

            result.ApplyProperty(data);
            EditorUtility.SetDirty(result);

            return result;
        } 

        protected virtual void ApplyProperty(Database.SpreadSheetData.CellEventData data)
        {
            this.category = (Constants.CellEventCategory)Enum.Parse(typeof(Constants.CellEventCategory), data.Category);
            this.condition = AssetDatabase.LoadAssetAtPath<CellEventGenerateCondition>($"Assets/HK/AutoAnt/DataSources/CellEvents/Conditions/{data.Condition}.asset");
            this.size = data.Size;
            this.constructionSE = AssetDatabase.LoadAssetAtPath<AudioClip>($"Assets/HK/AutoAnt/DataSources/SE/{data.Constructionse}.mp3");
            this.destructionSE = AssetDatabase.LoadAssetAtPath<AudioClip>($"Assets/HK/AutoAnt/DataSources/SE/{data.Destructionse}.mp3");
            this.constructionEffect = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/HK/AutoAnt/Prefabs/Effects/{data.Constructioneffect}.prefab").GetComponent<PoolableEffect>();
            this.destructionEffect = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/HK/AutoAnt/Prefabs/Effects/{data.Destructioneffect}.prefab").GetComponent<PoolableEffect>();
        }
#endif
    }
}
