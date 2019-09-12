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
    public class CellBundleData : IRecord
    {
[SerializeField]
int id;
public int Id { get {return id; } set { id = value;} }

[SerializeField]
int group;
public int Group { get {return group; } set { group = value;} }

[SerializeField]
int cellrecordid;
public int Cellrecordid { get {return cellrecordid; } set { cellrecordid = value;} }

[SerializeField]
int x;
public int X { get {return x; } set { x = value;} }

[SerializeField]
int y;
public int Y { get {return y; } set { y = value;} }

[SerializeField]
int width;
public int Width { get {return width; } set { width = value;} }

[SerializeField]
int height;
public int Height { get {return height; } set { height = value;} }

    }
}
