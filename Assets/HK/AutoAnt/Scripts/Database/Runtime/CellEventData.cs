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
    public class CellEventData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
string classname;
public string Classname { get {return classname; } set { classname = value;} }

[SerializeField]
string name;
public string Name { get {return name; } set { name = value;} }

[SerializeField]
string category;
public string Category { get {return category; } set { category = value;} }

[SerializeField]
string condition;
public string Condition { get {return condition; } set { condition = value;} }

[SerializeField]
int size;
public int Size { get {return size; } set { size = value;} }

[SerializeField]
string constructionse;
public string Constructionse { get {return constructionse; } set { constructionse = value;} }

[SerializeField]
string destructionse;
public string Destructionse { get {return destructionse; } set { destructionse = value;} }

[SerializeField]
string constructioneffect;
public string Constructioneffect { get {return constructioneffect; } set { constructioneffect = value;} }

[SerializeField]
string destructioneffect;
public string Destructioneffect { get {return destructioneffect; } set { destructioneffect = value;} }

[SerializeField]
string gimmickprefab;
public string Gimmickprefab { get {return gimmickprefab; } set { gimmickprefab = value;} }

    }
}
