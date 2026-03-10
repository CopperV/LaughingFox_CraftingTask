using CopGameDev.LaughingFoxTest.Inventory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class RecipeItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TMP_Text itemNameText;

        [SerializeField]
        private TMP_Text itemDescriptionText;

        [SerializeField]
        private Image itemIconImage;

        [SerializeField]
        private RectTransform itemQuantityRect;

        [SerializeField]
        private TMP_Text itemQuantityLabel;

        private CraftingRecipe recipe;
        private DetailedItemView detailedItemView;
        private Action<CraftingRecipe> onClick;

        public void Setup(CraftingRecipe recipe, Action<CraftingRecipe> onClick, DetailedItemView itemView = null)
        {
            this.detailedItemView = itemView;
            this.onClick = onClick;

            this.recipe = recipe;
            var item = recipe.Result.Item;

            itemNameText.text = item.ItemName;
            itemDescriptionText.text = item.Description;
            itemIconImage.sprite = item.Icon;
            if (recipe.Result.amount > 1)
            {
                itemQuantityRect.gameObject.SetActive(true);
                itemQuantityLabel.text = recipe.Result.amount.ToString();
            }
            else
            {
                itemQuantityRect.gameObject.SetActive(false);
            }

            button.onClick.AddListener(OnClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Show(recipe.Result.Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Hide();
        }

        public void ResetView()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => onClick?.Invoke(recipe);
    }
}
