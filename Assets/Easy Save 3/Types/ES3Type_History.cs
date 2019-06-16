using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("generateCellEvent")]
	public class ES3Type_History : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_History() : base(typeof(HK.AutoAnt.UserControllers.History)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.History)obj;
			
			writer.WritePrivateField("generateCellEvent", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.History)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "generateCellEvent":
					reader.SetPrivateField("generateCellEvent", reader.Read<HK.AutoAnt.UserControllers.GenerateCellEventHistory>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.History();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_HistoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_HistoryArray() : base(typeof(HK.AutoAnt.UserControllers.History[]), ES3Type_History.Instance)
		{
			Instance = this;
		}
	}
}