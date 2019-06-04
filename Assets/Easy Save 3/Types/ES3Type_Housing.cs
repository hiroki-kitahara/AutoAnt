using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("BasePopulationAmount", "CurrentPopulation", "Level", "size", "Origin", "name")]
	public class ES3Type_Housing : ES3ScriptableObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Housing() : base(typeof(HK.AutoAnt.CellControllers.Events.Housing)){ Instance = this; }

		protected override void WriteScriptableObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Housing)obj;
			
			writer.WriteProperty("BasePopulationAmount", instance.BasePopulationAmount, ES3Type_int.Instance);
			writer.WriteProperty("CurrentPopulation", instance.CurrentPopulation, ES3Type_int.Instance);
			writer.WriteProperty("Level", instance.Level, ES3Type_int.Instance);
			writer.WritePrivateField("size", instance);
			writer.WritePrivateProperty("Origin", instance);
			writer.WriteProperty("name", instance.name, ES3Type_string.Instance);
		}

		protected override void ReadScriptableObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.CellControllers.Events.Housing)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "BasePopulationAmount":
						instance.BasePopulationAmount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "CurrentPopulation":
						instance.CurrentPopulation = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Level":
						instance.Level = reader.Read<System.Int32>(ES3Type_int.Instance);
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

	public class ES3Type_HousingArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_HousingArray() : base(typeof(HK.AutoAnt.CellControllers.Events.Housing[]), ES3Type_Housing.Instance)
		{
			Instance = this;
		}
	}
}