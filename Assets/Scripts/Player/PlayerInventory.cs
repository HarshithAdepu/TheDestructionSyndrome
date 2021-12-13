using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<Weapon> availableWeapons;
    [SerializeField] List<InventoryItem> inventoryItems;
    void Start()
    {
        availableWeapons = new List<Weapon>();
    }

    void Update()
    {

    }

    public void PickupItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.HEALTH: break;
            case InventoryItem.ItemType.ARMOR: break;
            case InventoryItem.ItemType.SHOTGUN_AMMO: break;
            case InventoryItem.ItemType.AR_AMMO: break;
            case InventoryItem.ItemType.COIN: break;
            default: break;
        }

    }
}
