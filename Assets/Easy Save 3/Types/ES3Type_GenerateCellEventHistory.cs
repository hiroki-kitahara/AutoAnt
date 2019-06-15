using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("histories")]
	public class ES3Type_GenerateCellEventHistory : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_GenerateCellEventHistory() : base(typeof(HK.AutoAnt.UserControllers.GenerateCellEventHistory)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.GenerateCellEventHistory)obj;
			
			writer.WritePrivateField("histories", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.GenerateCellEventHistory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "histories":
					reader.SetPrivateField("histories", reader.Read<System.Collections.Generic.Dictionary<System.Int32, HK.AutoAnt.UserControllers.GenerateCellEventHistory.CellEvent>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.GenerateCellEventHistory();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_GenerateCellEventHistoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_GenerateCellEventHistoryArray() : base(typeof(HK.AutoAnt.UserControllers.GenerateCellEventHistory[]), ES3Type_GenerateCellEventHistory.Instance)
		{
			Instance = this;
		}
	}
}