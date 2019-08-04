using UnityEngine;
using System.Collections;
using HK.AutoAnt.Constants;

namespace HK.AutoAnt.Database.SpreadSheetData
{
    ///
    /// !!! Machine generated code !!!
    /// !!! DO NOT CHANGE Tabs to Spaces !!!
    ///
    [System.Serializable]
    public class ItemData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
string name;
public string Name { get {return name; } set { name = value;} }

[SerializeField]
string icon;
public string Icon { get {return icon; } set { icon = value;} }

[SerializeField]
int stacknumber;
public int Stacknumber { get {return stacknumber; } set { stacknumber = value;} }

    }
}
