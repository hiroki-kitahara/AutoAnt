using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("nextPopulation")]
	public class ES3Type_UnlockCellBundle : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_UnlockCellBundle() : base(typeof(HK.AutoAnt.UserControllers.UnlockCellBundle)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.UnlockCellBundle)obj;
			
			writer.WritePrivateField("nextPopulation", instance);
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