using CopGameDev.LaughingFoxTest.Inventory;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class DefaultCraftingService : ICraftingService
    {
        public bool CanCraft(IInventory inventory, CraftingRecipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!inventory.HasItem(ingredient.Item, ingredient.amount))
                    return false;
            }
            return true;
        }

        public bool Craft(IInventory inventory, CraftingRecipe recipe)
        {
            if (!CanCraft(inventory, recipe))
                return false;

            foreach (var ingredient in recipe.Ingredients)
            {
                inventory.RemoveItem(ingredient.Item, ingredient.amount);
            }

            inventory.AddItem(recipe.Result.Item, recipe.Result.amount);
            return true;
        }
    }
}
