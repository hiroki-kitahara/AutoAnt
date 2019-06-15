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
    public class LevelUpCostData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
int level;
public int Level { get {return level; } set { level = value;} }

[SerializeField]
double money;
public double Money { get {return money; } set { money = value;} }

[SerializeField]
string items;
public string Items { get {return items; } set { items = value;} }

    }
}
