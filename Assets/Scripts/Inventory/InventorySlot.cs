using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Sprite hoverSprite;

        [SerializeField]
        private Image itemIcon;

        [SerializeField]
        private RectTransform itemQuantityRect;

        [SerializeField]
        private TMP_Text itemQuantityLabel;

        private InventoryItem item;
        private DetailedItemView detailedItemView;

        private Sprite originalSprite;

        private void Start()
        {
            originalSprite = backgroundImage.sprite;
        }

        private void OnDisable()
        {
            backgroundImage.sprite = originalSprite;
        }

        public void SetupSlot(InventoryItem item, DetailedItemView itemView = null)
        {
            this.item = item;
            this.detailedItemView = itemView;

            itemIcon.sprite = item.Item.Icon;

            if(item.amount > 1)
            {
                itemQuantityRect.gameObject.SetActive(true);
                itemQuantityLabel.text = item.amount.ToString();
            }
            else
            {
                itemQuantityRect.gameObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Show(item.Item);

            originalSprite = backgroundImage.sprite;
            backgroundImage.sprite = hoverSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (detailedItemView != null)
                detailedItemView.Hide();

            backgroundImage.sprite = originalSprite;
        }
    }
}
