using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("Wallet", "Inventory", "GenerateCellEventHistory", "UnlockCellEvent")]
	public class ES3Type_SerializableUser : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_SerializableUser() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableUser)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableUser)obj;
			
			writer.WriteProperty("Wallet", instance.Wallet, ES3Type_SerializableWallet.Instance);
			writer.WriteProperty("Inventory", instance.Inventory, ES3Type_Inventory.Instance);
			writer.WriteProperty("GenerateCellEventHistory", instance.GenerateCellEventHistory, ES3Type_GenerateCellEventHistory.Instance);
			writer.WriteProperty("UnlockCellEvent", instance.UnlockCellEvent);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.SaveData.Serializables.SerializableUser)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "Wallet":
						instance.Wallet = reader.Read<HK.AutoAnt.SaveData.Serializables.SerializableWallet>(ES3Type_SerializableWallet.Instance);
						break;
					case "Inventory":
						instance.Inventory = reader.Read<HK.AutoAnt.UserControllers.Inventory>(ES3Type_Inventory.Instance);
						break;
					case "GenerateCellEventHistory":
						instance.GenerateCellEventHistory = reader.Read<HK.AutoAnt.UserControllers.GenerateCellEventHistory>(ES3Type_GenerateCellEventHistory.Instance);
						break;
					case "UnlockCellEvent":
						instance.UnlockCellEvent = reader.Read<HK.AutoAnt.UserControllers.UnlockCellEvent>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.SaveData.Serializables.SerializableUser();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_SerializableUserArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_SerializableUserArray() : base(typeof(HK.AutoAnt.SaveData.Serializables.SerializableUser[]), ES3Type_SerializableUser.Instance)
		{
			Instance = this;
		}
	}
}