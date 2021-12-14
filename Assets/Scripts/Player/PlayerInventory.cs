using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;
    [SerializeField] List<InventoryItem> inventoryItems;

    public void PickupItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryItem.ItemType.HEALTH:
                if (PlayerHealthManager.playerHealthManagerInstance.Heal(item.itemCount))
                {
                    item.gameObject.SetActive(false);
                }
                break;

            case InventoryItem.ItemType.ARMOR:
                if (PlayerHealthManager.playerHealthManagerInstance.ArmorUp(item.itemCount))
                {
                    item.gameObject.SetActive(false);
                }
                break;

            case InventoryItem.ItemType.SHOTGUN_AMMO:
                break;
            case InventoryItem.ItemType.AR_AMMO: break;
            case InventoryItem.ItemType.COIN: break;
            case InventoryItem.ItemType.GRENADE: break;
            default: break;
        }

    }
}
