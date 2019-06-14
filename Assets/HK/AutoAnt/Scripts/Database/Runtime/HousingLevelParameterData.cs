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
    public class HousingLevelParameterData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
int level;
public int Level { get {return level; } set { level = value;} }

[SerializeField]
double population;
public double Population { get {return population; } set { population = value;} }

    }
}
