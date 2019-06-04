using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("PopularityAmount", "Level", "AcquireItemRecordId", "CurrentItemNumber", "size", "Origin", "name")]
	public class ES3Type_Facility : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Facility() : base(typeof(HK.AutoAnt.CellControllers.Events.Facility)){ Instance = this; }

		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Facility)obj;
			
			writer.WriteProperty("PopularityAmount", instance.PopularityAmount, ES3Type_int.Instance);
			writer.WriteProperty("Level", instance.Level, ES3Type_int.Instance);
			writer.WriteProperty("AcquireItemRecordId", instance.AcquireItemRecordId, ES3Type_int.Instance);
			writer.WriteProperty("CurrentItemNumber", instance.CurrentItemNumber, ES3Type_int.Instance);
			writer.WritePrivateField("size", instance);
			writer.WritePrivateProperty("Origin", instance);
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Facility)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "PopularityAmount":
						instance.PopularityAmount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Level":
						instance.Level = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "AcquireItemRecordId":
						instance.AcquireItemRecordId = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "CurrentItemNumber":
						instance.CurrentItemNumber = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "size":
					reader.SetPrivateField("size", reader.Read<System.Int32>(), instance);
					break;
					case "Origin":
					reader.SetPrivateProperty("Origin", reader.Read<UnityEngine.Vector2Int>(), instance);
					break;
					case "name":
						instance.name = reader.Read<System.String>(ES3Type_string.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}

	public class ES3Type_FacilityArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_FacilityArray() : base(typeof(HK.AutoAnt.CellControllers.Events.Facility[]), ES3Type_Facility.Instance)
		{
			Instance = this;
		}
	}
}