using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public SlotTag itemTag;
    public int buildType; 
}
