using CopGameDev.LaughingFoxTest.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    [Serializable]
    public class DefaultInventory : IInventory
    {
        private Dictionary<ItemData, int> items = new();

        private List<InventoryItem> cachedItems = new();
        public IReadOnlyCollection<InventoryItem> Items => items.Select(kvp => new InventoryItem(kvp.Key, kvp.Value)).ToList();

        public event Action InventoryModifyEvent;
        public event Action<InventoryItem> ItemAddedToInventoryEvent;
        public event Action<InventoryItem> ItemRemovedFromInventoryEvent;

        public bool CanAddItem(ItemData item, int amount = 1) => true;

        public bool HasItem(ItemData item, int amount = 1)
        {
            if (amount < 1)
                return false;

            return items.TryGetValue(item, out int currentAmount) && currentAmount >= amount;
        }

        public bool AddItem(ItemData item, int amount = 1)
        {
            if(amount < 1)
                return false;

            if(!CanAddItem(item, amount))
                return false;

            int currentAmount = items.GetValueOrDefault(item, 0);
            items[item] = currentAmount + amount;

            UpdateCachedItems();

            InventoryModifyEvent?.Invoke();
            ItemAddedToInventoryEvent?.Invoke(new(item, amount));

            return true;
        }

        public bool RemoveItem(ItemData item, int amount = 1)
        {
            if(!HasItem(item, amount))
                return false;

            items[item] -= amount;
            if(items[item] <= 0)
                items.Remove(item);

            UpdateCachedItems();

            InventoryModifyEvent?.Invoke();
            ItemRemovedFromInventoryEvent?.Invoke(new(item, amount));

            return true;
        }

        private void UpdateCachedItems()
        {
            cachedItems.Clear();
            cachedItems.AddRange(items.Select(kvp => new InventoryItem(kvp.Key, kvp.Value)).ToList());
        }
    }
}
