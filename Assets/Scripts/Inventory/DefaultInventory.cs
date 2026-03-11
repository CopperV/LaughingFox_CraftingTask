using CopGameDev.LaughingFoxTest.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    [Serializable]
    public class DefaultInventory : IInventory
    {
        private Dictionary<ItemData, InventoryItem> items = new();
        public IReadOnlyCollection<InventoryItem> Items => items.Values;

        public event Action InventoryModifyEvent;
        public event Action<InventoryItem> ItemAddedToInventoryEvent;
        public event Action<InventoryItem> ItemRemovedFromInventoryEvent;

        public bool CanAddItem(ItemData item, int amount = 1) => true;

        public bool HasItem(ItemData item, int amount = 1)
        {
            if (amount < 1)
                return false;

            return items.TryGetValue(item, out var invItem) && invItem.amount >= amount;
        }

        public bool AddItem(ItemData item, int amount = 1)
        {
            if(amount < 1)
                return false;

            if(!CanAddItem(item, amount))
                return false;

            if(items.TryGetValue(item, out var invItem))
            {
                items[item].amount += amount;
            }
            else
            {
                invItem = new InventoryItem(item, amount);
                items[item] = invItem;
            }

            InventoryModifyEvent?.Invoke();
            ItemAddedToInventoryEvent?.Invoke(new(item, amount));

            return true;
        }

        public bool RemoveItem(ItemData item, int amount = 1)
        {
            if(!HasItem(item, amount))
                return false;

            items[item].amount -= amount;
            if(items[item].amount <= 0)
                items.Remove(item);

            InventoryModifyEvent?.Invoke();
            ItemRemovedFromInventoryEvent?.Invoke(new(item, amount));

            return true;
        }
    }
}
