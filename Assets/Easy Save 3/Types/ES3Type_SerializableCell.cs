using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("RecordId", "Position", "Group")]
	public class ES3Type_SerializableCell : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_SerializableCell() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableCell)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableCell)obj;
			
			writer.WriteProperty("RecordId", instance.RecordId, ES3Type_int.Instance);
			writer.WriteProperty("Position", instance.Position, ES3Type_Vector2Int.Instance);
			writer.WriteProperty("Group", instance.Group, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableCell)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "RecordId":
						instance.RecordId = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Position":
						instance.Position = reader.Read<UnityEngine.Vector2Int>(ES3Type_Vector2Int.Instance);
						break;
					case "Group":
						instance.Group = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.SaveData.Serializables.SerializableCell();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SerializableCellArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SerializableCellArray() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableCell[]), ES3Type_SerializableCell.Instance)
		{
			Instance = this;
		}
	}
}