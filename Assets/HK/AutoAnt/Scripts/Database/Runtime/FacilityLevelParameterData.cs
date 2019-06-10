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
    public class FacilityLevelParameterData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
int level;
public int Level { get {return level; } set { level = value;} }

[SerializeField]
int productslot;
public int Productslot { get {return productslot; } set { productslot = value;} }

[SerializeField]
string productname;
public string Productname { get {return productname; } set { productname = value;} }

[SerializeField]
float needproducttime;
public float Needproducttime { get {return needproducttime; } set { needproducttime = value;} }

[SerializeField]
int popularity;
public int Popularity { get {return popularity; } set { popularity = value;} }

    }
}
