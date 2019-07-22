using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("BGMVolume", "SEVolume")]
	public class ES3Type_Option : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Option() : base(typeof(HK.AutoAnt.UserControllers.Option)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.Option)obj;
			
			writer.WriteProperty("BGMVolume", instance.BGMVolume, ES3Type_float.Instance);
			writer.WriteProperty("SEVolume", instance.SEVolume, ES3Type_float.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.Option)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "BGMVolume":
						instance.BGMVolume = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "SEVolume":
						instance.SEVolume = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.Option();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_OptionArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_OptionArray() : base(typeof(HK.AutoAnt.UserControllers.Option[]), ES3Type_Option.Instance)
		{
			Instance = this;
		}
	}
}