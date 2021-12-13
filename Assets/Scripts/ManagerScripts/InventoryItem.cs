using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public enum ItemType { HEALTH, ARMOR, SHOTGUN_AMMO, AR_AMMO, COIN, WEAPON };
    public ItemType itemType;
    public string itemCount;
}
