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

    public void Show(CraftingRecipe recipe, DetailedItemView itemView = null)
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
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

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
