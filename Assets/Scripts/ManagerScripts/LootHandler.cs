using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHandler : MonoBehaviour
{
    public static LootHandler lootHandlerInstance;
    void Start()
    {
        lootHandlerInstance = this;
    }

    public InventoryItem DropLoot()
    {
        return null;
    }
}
