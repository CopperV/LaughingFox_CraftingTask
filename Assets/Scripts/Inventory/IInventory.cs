using CopGameDev.LaughingFoxTest.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public interface IInventory
    {
        bool CanAddItem(ItemData item, int amount = 1);
        bool AddItem(ItemData item, int amount = 1);
        bool RemoveItem(ItemData item, int amount = 1);
        bool HasItem(ItemData item, int amount = 1);

        IReadOnlyCollection<InventoryItem> Items { get; }

        event Action InventoryModifyEvent;
        event Action<InventoryItem> ItemAddedToInventoryEvent;
        event Action<InventoryItem> ItemRemovedFromInventoryEvent;
    }

    [Serializable]
    public class InventoryItem
    {
        [field: SerializeField]
        public ItemData Item { get; private set; }

        public int amount;

        public InventoryItem(ItemData item, int amount)
        {
            Item = item;
            this.amount = amount;
        }
    }
}
