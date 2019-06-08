using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("Money")]
	public class ES3Type_SerializableWallet : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_SerializableWallet() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableWallet)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableWallet)obj;
			
			writer.WriteProperty("Money", instance.Money, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableWallet)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Money":
						instance.Money = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.SaveData.Serializables.SerializableWallet();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SerializableWalletArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SerializableWalletArray() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableWallet[]), ES3Type_SerializableWallet.Instance)
		{
			Instance = this;
		}
	}
}