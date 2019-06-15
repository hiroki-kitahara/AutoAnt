using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("numbers")]
	public class ES3Type_CellEvent : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_CellEvent() : base(typeof(HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent)obj;
			
			writer.WritePrivateField("numbers", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "numbers":
					reader.SetPrivateField("numbers", reader.Read<System.Collections.Generic.List<System.Int32>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_CellEventArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_CellEventArray() : base(typeof(HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent[]), ES3Type_CellEvent.Instance)
		{
			Instance = this;
		}
	}
}