using CopGameDev.LaughingFoxTest.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class RecipeListView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform recipeListContainer;

        [SerializeField]
        private GameObject recipePrefab;

        public void Setup(IEnumerable<CraftingRecipe> recipes, Action<CraftingRecipe> onRecipeClick, DetailedItemView itemView = null)
        {
            foreach (var recipe in recipes)
            {
                var recipeView = Instantiate(recipePrefab, recipeListContainer).GetComponent<RecipeItemView>();
                recipeView.Setup(recipe, onRecipeClick, itemView);
            }
        }

        public void Clear()
        {
            foreach (Transform child in recipeListContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
