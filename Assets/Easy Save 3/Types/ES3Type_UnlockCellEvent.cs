using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("Elements")]
	public class ES3Type_UnlockCellEvents : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_UnlockCellEvents() : base(typeof(HK.AutoAnt.UserControllers.UnlockCellEvent)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.UnlockCellEvent)obj;
			
			writer.WritePrivateProperty("Elements", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.UnlockCellEvent)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Elements":
					reader.SetPrivateProperty("Elements", reader.Read<System.Collections.Generic.List<System.Int32>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.UnlockCellEvent();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_UnlockCellEventsArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_UnlockCellEventsArray() : base(typeof(HK.AutoAnt.UserControllers.UnlockCellEvent[]), ES3Type_UnlockCellEvents.Instance)
		{
			Instance = this;
		}
	}
}