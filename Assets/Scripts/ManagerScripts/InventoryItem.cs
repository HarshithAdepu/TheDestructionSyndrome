using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public enum ItemType { HEALTH, ARMOR, SHOTGUN_AMMO, AR_AMMO, COIN, WEAPON, GRENADE };
    public ItemType itemType;
    public int itemCount;
    public Vector2Int dropRange;
}
