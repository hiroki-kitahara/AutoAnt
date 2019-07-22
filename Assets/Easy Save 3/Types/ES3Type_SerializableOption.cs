using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("<BGMVolume>k__BackingField", "<SEVolume>k__BackingField")]
	public class ES3Type_SerializableOption : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_SerializableOption() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableOption)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableOption)obj;
			
			writer.WritePrivateField("<BGMVolume>k__BackingField", instance);
			writer.WritePrivateField("<SEVolume>k__BackingField", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableOption)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "<BGMVolume>k__BackingField":
					reader.SetPrivateField("<BGMVolume>k__BackingField", reader.Read<System.Single>(), instance);
					break;
					case "<SEVolume>k__BackingField":
					reader.SetPrivateField("<SEVolume>k__BackingField", reader.Read<System.Single>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.SaveData.Serializables.SerializableOption();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SerializableOptionArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SerializableOptionArray() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableOption[]), ES3Type_SerializableOption.Instance)
		{
			Instance = this;
		}
	}
}