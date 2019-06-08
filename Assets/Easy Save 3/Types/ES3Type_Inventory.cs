using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("items")]
	public class ES3Type_Inventory : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_Inventory() : base(typeof(HK.AutoAnt.UserControllers.Inventory)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.UserControllers.Inventory)obj;
			
			writer.WritePrivateField("items", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.UserControllers.Inventory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "items":
					reader.SetPrivateField("items", reader.Read<System.Collections.Generic.Dictionary<System.Int32, System.Int32>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.UserControllers.Inventory();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_InventoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_InventoryArray() : base(typeof(HK.AutoAnt.UserControllers.Inventory[]), ES3Type_Inventory.Instance)
		{
			Instance = this;
		}
	}
}