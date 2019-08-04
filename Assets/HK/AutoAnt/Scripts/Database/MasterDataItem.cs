using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// アイテムのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/Item")]
    public sealed class MasterDataItem : MasterDataBase<MasterDataItem.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordName
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private StringAsset.Finder name = null;
            public string Name => this.name.Get;

            [SerializeField]
            private Texture2D icon = null;
            public Texture2D Icon => this.icon;

            /// <summary>
            /// <see cref="Icon"/>を<see cref="Sprite"/>に変換して返す
            /// </summary>
            public Sprite IconToSprite
            {
                get
                {
                    if(this.cachedIconToSprite == null)
                    {
                        this.cachedIconToSprite = Sprite.Create(this.icon, new Rect(0, 0, this.icon.width, this.icon.height), Vector2.zero);
                    }

                    return this.cachedIconToSprite;
                }
            }
            private Sprite cachedIconToSprite = null;

#if UNITY_EDITOR
            public Record(SpreadSheetData.ItemData data)
            {
                this.id = data.Id;
                var stringAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/HK/AutoAnt/DataSources/StringAsset/Item.asset");
                this.name = stringAsset.CreateFinderSafe(data.Name);
                this.icon = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/HK/AutoAnt/Textures/Icon/{data.Icon}.png");
            }
#endif
        }
    }
}
