using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemScriptables : ScriptableObject

{
    public int itemID;
    public string itemName = "New Item";
    public Sprite itemIcon = null;
    public bool isUsable = false;
    public bool isConsumable = false;
}
