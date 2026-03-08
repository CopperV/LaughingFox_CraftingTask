using CopGameDev.LaughingFoxTest.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class CraftingPanelView : UIPanelBase, IInventoryView
    {
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
            OnShow?.Invoke();
        }

        private void HideView()
        {
            ClearView();
            OnHide?.Invoke();
        }

        public void RefreshView()
        {
            ClearView();

            var recipes = RecipesManager.Instance.GetRecipesByCategory(category);
            recipeListView.Setup(recipes, recipe => recipeView.Show(recipe, detailedItemView), detailedItemView);
        }

        private void ClearView()
        {
            recipeView.Hide();
            recipeListView.Clear();
        }
    }
}
