using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("size", "Items", "Origin", "name")]
	public class ES3Type_Chest : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Chest() : base(typeof(HK.AutoAnt.CellControllers.Events.Chest)){ Instance = this; }

		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Chest)obj;
			
			writer.WritePrivateField("size", instance);
			writer.WritePrivateProperty("Items", instance);
			writer.WritePrivateProperty("Origin", instance);
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Chest)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "size":
					reader.SetPrivateField("size", reader.Read<System.Int32>(), instance);
					break;
					case "Items":
					reader.SetPrivateProperty("Items", reader.Read<System.Collections.Generic.List<System.Int32>>(), instance);
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

	public class ES3Type_ChestArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_ChestArray() : base(typeof(HK.AutoAnt.CellControllers.Events.Chest[]), ES3Type_Chest.Instance)
		{
			Instance = this;
		}
	}
}