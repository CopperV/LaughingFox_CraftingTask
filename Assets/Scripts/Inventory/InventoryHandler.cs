using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        [SerializeReference]
        private IInventory inventory = new DefaultInventory();

        [SerializeField]
        private List<InventoryItem> defaultInventory = new();

        public IInventory Inventory => inventory;

        private void Awake()
        {
            foreach (var item in defaultInventory)
            {
                inventory.AddItem(item.Item, item.amount);
            }
        }
    }
}