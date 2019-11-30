using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Food,
    Equipment,
    KeyItem
}

public class ItemObject : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    [TextArea]
    public string description;
    public float restoreHealthValue;
}
