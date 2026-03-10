using CopGameDev.LaughingFoxTest.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class RecipeListView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform recipeListContainer;

        [SerializeField]
        private RecipeItemView recipePrefab;

        private ObjectPool<RecipeItemView> recipePool;
        private List<RecipeItemView> activeRecipes = new();

        private void Awake()
        {
            recipePool = new(
                createFunc: () => Instantiate(recipePrefab, recipeListContainer),
                actionOnGet: slot => slot.gameObject.SetActive(true),
                actionOnRelease: slot => {
                    slot.gameObject.SetActive(false);
                    slot.ResetView();
                },
                actionOnDestroy: slot => Destroy(slot.gameObject),
                collectionCheck: false,
                defaultCapacity: 50,
                maxSize: int.MaxValue
            );
        }

        public void Setup(IEnumerable<CraftingRecipe> recipes, Action<CraftingRecipe> onRecipeClick, DetailedItemView itemView = null)
        {
            Clear();
            foreach (var recipe in recipes)
            {
                var view = recipePool.Get();
                view.transform.SetAsLastSibling();
                view.Setup(recipe, onRecipeClick, itemView);
                activeRecipes.Add(view);
            }
        }

        public void Clear()
        {
            foreach (var recipe in activeRecipes)
            {
                recipePool.Release(recipe);
            }
            activeRecipes.Clear();
        }
    }
}
