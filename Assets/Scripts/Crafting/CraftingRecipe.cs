using CopGameDev.LaughingFoxTest.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Scriptable Objects/CraftingRecipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [field: SerializeField]
        public CraftingCategory Category { get; private set; }

        [field: SerializeField]
        public List<InventoryItem> Ingredients { get; private set; } = new();

        [field: SerializeField]
        public InventoryItem Result { get; private set; }
    }
}
