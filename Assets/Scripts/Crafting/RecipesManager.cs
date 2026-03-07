using System.Collections.Generic;
using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    [DefaultExecutionOrder(-100)]
    public class RecipesManager : MonoBehaviour
    {
        public static RecipesManager Instance { get; private set; }

        private HashSet<CraftingRecipe> recipes = new();

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Initialize();
        }

        private void Initialize()
        {
            foreach (var recipe in Resources.LoadAll<CraftingRecipe>("Crafting/Recipes"))
            {
                recipes.Add(recipe);
            }

            Debug.Log($"Loaded {recipes.Count} crafting recipes.");
        }

        public IEnumerable<CraftingRecipe> GetRecipesByCategory(CraftingCategory category)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Category == category)
                    yield return recipe;
            }
        }
    }
}
