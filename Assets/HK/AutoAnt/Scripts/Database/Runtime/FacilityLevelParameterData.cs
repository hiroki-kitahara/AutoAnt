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
int productid;
public int Productid { get {return productid; } set { productid = value;} }

[SerializeField]
float needproducttime;
public float Needproducttime { get {return needproducttime; } set { needproducttime = value;} }

[SerializeField]
int productnum;
public int Productnum { get {return productnum; } set { productnum = value;} }

[SerializeField]
double popularity;
public double Popularity { get {return popularity; } set { popularity = value;} }

[SerializeField]
double economic;
public double Economic { get {return economic; } set { economic = value;} }


    }
}
