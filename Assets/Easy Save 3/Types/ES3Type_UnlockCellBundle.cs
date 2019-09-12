using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("nextPopulation", "targetRecordIds")]
	public class ES3Type_UnlockCellBundle : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_UnlockCellBundle() : base(typeof(HK.AutoAnt.UserControllers.UnlockCellBundle)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.UnlockCellBundle)obj;
			
			writer.WritePrivateField("nextPopulation", instance);
			writer.WritePrivateField("targetRecordIds", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.UnlockCellBundle)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "nextPopulation":
					reader.SetPrivateField("nextPopulation", reader.Read<System.Double>(), instance);
					break;
					case "targetRecordIds":
					reader.SetPrivateField("targetRecordIds", reader.Read<System.Collections.Generic.List<System.Int32>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.UnlockCellBundle();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_UnlockCellBundleArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_UnlockCellBundleArray() : base(typeof(HK.AutoAnt.UserControllers.UnlockCellBundle[]), ES3Type_UnlockCellBundle.Instance)
		{
			Instance = this;
		}
	}
}