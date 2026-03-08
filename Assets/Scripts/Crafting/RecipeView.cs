using CopGameDev.LaughingFoxTest.Crafting;
using CopGameDev.LaughingFoxTest.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour
{
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

    public void Show(CraftingRecipe recipe, IInventory inventory, ICraftingService craftingService, DetailedItemView itemView = null)
    {
        Clear();
        
        resultItemView.Show(recipe.Result.Item);

        recipe.Ingredients.ForEach(ingredient =>
        {
            var slot = Instantiate(ingredientSlotPrefab, ingredientsContainer);
            slot.SetupSlot(ingredient, itemView);
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
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        craftButton.gameObject.SetActive(false);
        craftButton.interactable = false;
    }

    private void Clear()
    {
        foreach (Transform child in ingredientsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
