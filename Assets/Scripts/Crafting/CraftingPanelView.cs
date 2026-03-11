using CopGameDev.LaughingFoxTest.Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class CraftingPanelView : UIPanelBase, IInventoryView
    {
        [SerializeField]
        private InventoryHandler inventoryHandler;

        [SerializeField]
        private CraftingHandler craftingHandler;

        [SerializeField]
        private CraftingCategory category;

        [SerializeField]
        private DetailedItemView detailedItemView;

        [SerializeField]
        private RecipeListView recipeListView;

        [SerializeField]
        private RecipeView recipeView;

        [SerializeField]
        private UnityEvent OnShow;

        [SerializeField]
        private UnityEvent OnHide;

        private bool toggled = false;

        private void Start()
        {
            ToggleView(false);
            HideView();
        }

        public override void ToggleView(bool toggle)
        {
            if (toggled == toggle)
                return;

            toggled = toggle;

            if (toggled)
                ShowView();
            else
                HideView();
        }

        private void ShowView()
        {
            RefreshView();

            StartCoroutine(LayoutRebuildCoroutine());

            OnShow?.Invoke();
        }

        private void HideView()
        {
            if (detailedItemView != null)
                detailedItemView.Hide();

            StopAllCoroutines();

            ClearView();
            OnHide?.Invoke();
        }

        public void RefreshView()
        {
            ClearView();

            var recipes = RecipesManager.Instance.GetRecipesByCategory(category);
            recipeListView.Setup(recipes, recipe => recipeView.Show(recipe, inventoryHandler.Inventory, craftingHandler.CraftngService, detailedItemView), detailedItemView);
        }

        private void ClearView()
        {
            recipeView.Hide();
            recipeListView.Clear();
        }

        private IEnumerator LayoutRebuildCoroutine()
        {
            var rect = transform as RectTransform;

            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
}
