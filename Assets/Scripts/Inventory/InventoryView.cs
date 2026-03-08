using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CopGameDev.LaughingFoxTest.Inventory
{
    public interface IInventoryView : IUIPanel
    {
        void RefreshView();
    }

    public class InventoryView : UIPanelBase, IInventoryView
    {
        [SerializeField]
        private InventoryHandler inventoryHandler;

        [SerializeField]
        private RectTransform slotsContainer;

        [SerializeField]
        private InventorySlot slotPrefab;

        [SerializeField]
        private DetailedItemView detailedItemView;

        [SerializeField]
        private UnityEvent OnShow;

        [SerializeField]
        private UnityEvent OnHide;

        private bool toggled = false;

        private void Start()
        {
            inventoryHandler.Inventory.InventoryModifyEvent += UpdateView;

            ToggleView(false);
            HideView();
        }

        private void OnDestroy()
        {
            inventoryHandler.Inventory.InventoryModifyEvent -= UpdateView;
        }

        private void UpdateView()
        {
            if (toggled)
                RefreshView();
        }

        public override void ToggleView(bool toggle)
        {
            if(toggled == toggle)
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

            var items = inventoryHandler.Inventory.Items.
                OrderBy(entry => entry.Item.Category).
                ThenBy(entry => entry.Item.ItemName).
                ToList();

            foreach (var item in items)
            {
                var newSlot = Instantiate(slotPrefab, slotsContainer);
                newSlot.SetupSlot(item, detailedItemView);
            }
        }

        private void ClearView()
        {
            foreach (Transform child in slotsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
