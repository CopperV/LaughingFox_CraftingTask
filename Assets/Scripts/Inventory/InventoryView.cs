using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

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

        private ObjectPool<InventorySlot> slotPool;
        private List<InventorySlot> activeSlots = new();

        private bool toggled = false;

        private void Awake()
        {
            slotPool = new(
                createFunc: () => Instantiate(slotPrefab, slotsContainer),
                actionOnGet: slot => slot.gameObject.SetActive(true),
                actionOnRelease: slot => slot.gameObject.SetActive(false),
                actionOnDestroy: slot => Destroy(slot.gameObject),
                collectionCheck: false,
                defaultCapacity: 50,
                maxSize: int.MaxValue
            );
        }

        private void Start()
        {
            inventoryHandler.Inventory.InventoryModifyEvent += UpdateView;

            ToggleView(false);
            HideView();
        }

        private void OnDestroy()
        {
            inventoryHandler.Inventory.InventoryModifyEvent -= UpdateView;
            slotPool.Clear();
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
            if (detailedItemView != null)
                detailedItemView.Hide();

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
                var newSlot = slotPool.Get();
                newSlot.transform.SetAsLastSibling();
                newSlot.SetupSlot(item, detailedItemView);
                activeSlots.Add(newSlot);
            }
        }

        private void ClearView()
        {
            foreach (var slot in activeSlots)
            {
                slotPool.Release(slot);
            }
            activeSlots.Clear();
        }
    }
}
