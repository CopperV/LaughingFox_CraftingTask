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

        public void Setup(CraftingRecipe recipe, Action<CraftingRecipe> onClick, DetailedItemView itemView = null)
        {
            this.detailedItemView = itemView;

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

            button.onClick.AddListener(() => onClick?.Invoke(recipe));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Show(recipe.Result.Item);

            //originalSprite = backgroundImage.sprite;
            //backgroundImage.sprite = hoverSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Hide();

            //backgroundImage.sprite = originalSprite;
        }
    }
}
