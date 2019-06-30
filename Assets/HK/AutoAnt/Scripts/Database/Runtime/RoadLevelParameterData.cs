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
    public class RoadLevelParameterData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
int level;
public int Level { get {return level; } set { level = value;} }

[SerializeField]
int impactrange;
public int Impactrange { get {return impactrange; } set { impactrange = value;} }

[SerializeField]
float effectrate;
public float Effectrate { get {return effectrate; } set { effectrate = value;} }

    }
}
