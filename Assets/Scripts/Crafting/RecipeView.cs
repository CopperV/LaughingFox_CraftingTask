using CopGameDev.LaughingFoxTest.Crafting;
using CopGameDev.LaughingFoxTest.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class RecipeView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private DetailedItemView resultItemView;

        [SerializeField]
        private RectTransform ingredientsContainer;

        [SerializeField]
        private InventorySlot ingredientSlotPrefab;

        [SerializeField]
        private Button craftButton;

        private ObjectPool<InventorySlot> slotPool;
        private List<InventorySlot> activeSlots = new();

        private void Awake()
        {
            slotPool = new(
                createFunc: () => Instantiate(ingredientSlotPrefab, ingredientsContainer),
                actionOnGet: slot => slot.gameObject.SetActive(true),
                actionOnRelease: slot => slot.gameObject.SetActive(false),
                actionOnDestroy: slot => Destroy(slot.gameObject),
                collectionCheck: false,
                defaultCapacity: 5,
                maxSize: int.MaxValue
            );
        }

        public void Show(CraftingRecipe recipe, IInventory inventory, ICraftingService craftingService, DetailedItemView itemView = null)
        {
            Clear();

            resultItemView.Show(recipe.Result.Item);

            recipe.Ingredients.ForEach(ingredient =>
            {
                var slot = slotPool.Get();
                slot.transform.SetAsLastSibling();
                slot.SetupSlot(ingredient, itemView);
                activeSlots.Add(slot);
            });

            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            craftButton.gameObject.SetActive(true);
            craftButton.interactable = craftingService.CanCraft(inventory, recipe);
            craftButton.onClick.AddListener(() =>
            {
                if (!craftingService.CanCraft(inventory, recipe))
                    return;

                craftingService.Craft(inventory, recipe);
                craftButton.interactable = craftingService.CanCraft(inventory, recipe);
            });

            StartCoroutine(LayoutRebuildCoroutine());
        }

        public void Hide()
        {
            StopAllCoroutines();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            craftButton.gameObject.SetActive(false);
            craftButton.interactable = false;
        }

        private void Clear()
        {
            foreach (var slot in activeSlots)
            {
                slotPool.Release(slot);
            }
            activeSlots.Clear();
        }

        private IEnumerator LayoutRebuildCoroutine()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(root);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(root);
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        }
    }
}
