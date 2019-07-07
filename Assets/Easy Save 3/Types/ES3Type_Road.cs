using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("size", "Level", "Origin", "name")]
	public class ES3Type_Road : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Road() : base(typeof(HK.AutoAnt.CellControllers.Events.Road)){ Instance = this; }

		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Road)obj;
			
			writer.WritePrivateField("size", instance);
			writer.WriteProperty("Level", instance.Level, ES3Type_int.Instance);
			writer.WritePrivateProperty("Origin", instance);
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Road)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "size":
					reader.SetPrivateField("size", reader.Read<System.Int32>(), instance);
					break;
					case "Level":
						instance.Level = reader.Read<System.Int32>(ES3Type_int.Instance);
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

	public class ES3Type_RoadArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_RoadArray() : base(typeof(HK.AutoAnt.CellControllers.Events.Road[]), ES3Type_Road.Instance)
		{
			Instance = this;
		}
	}
}