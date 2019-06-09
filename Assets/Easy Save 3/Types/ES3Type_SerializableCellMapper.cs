using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("Cells", "CellEvents")]
	public class ES3Type_SerializableCellMapper : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_SerializableCellMapper() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableCellMapper)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableCellMapper)obj;
			
			writer.WriteProperty("Cells", instance.Cells);
			writer.WriteProperty("CellEvents", instance.CellEvents);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableCellMapper)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Cells":
						instance.Cells = reader.Read<System.Collections.Generic.List<HK.AutoAnt.SaveData.Serializables.SerializableCell>>();
						break;
					case "CellEvents":
						instance.CellEvents = reader.Read<System.Collections.Generic.List<HK.AutoAnt.CellControllers.Events.ICellEvent>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.SaveData.Serializables.SerializableCellMapper();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SerializableCellMapperArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SerializableCellMapperArray() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableCellMapper[]), ES3Type_SerializableCellMapper.Instance)
		{
			Instance = this;
		}
	}
}