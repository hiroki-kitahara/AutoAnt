using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("ItemId", "Amount")]
	public class ES3Type_StackedItem : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_StackedItem() : base(typeof(HK.AutoAnt.GameControllers.StackedItem)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (HK.AutoAnt.GameControllers.StackedItem)obj;
			
			writer.WritePrivateProperty("ItemId", instance);
			writer.WritePrivateProperty("Amount", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (HK.AutoAnt.GameControllers.StackedItem)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "ItemId":
					reader.SetPrivateProperty("ItemId", reader.Read<System.Int32>(), instance);
					break;
					case "Amount":
					reader.SetPrivateProperty("Amount", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new HK.AutoAnt.GameControllers.StackedItem();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_StackedItemArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_StackedItemArray() : base(typeof(HK.AutoAnt.GameControllers.StackedItem[]), ES3Type_StackedItem.Instance)
		{
			Instance = this;
		}
	}
}