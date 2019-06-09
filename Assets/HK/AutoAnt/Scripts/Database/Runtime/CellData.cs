using UnityEngine;
using System.Collections;
using HK.AutoAnt.Constants;

namespace HK.AutoAnt.Database
{
    ///
    /// !!! Machine generated code !!!
    /// !!! DO NOT CHANGE Tabs to Spaces !!!
    ///
    [System.Serializable]
    public class CellData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
CellType celltype;
public CellType CELLTYPE { get {return celltype; } set { celltype = value;} }

[SerializeField]
string prefabname;
public string Prefabname { get {return prefabname; } set { prefabname = value;} }

    }
}
