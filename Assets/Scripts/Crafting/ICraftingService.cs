using CopGameDev.LaughingFoxTest.Inventory;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public interface ICraftingService
    {
        bool CanCraft(IInventory inventory, CraftingRecipe recipe);
        bool Craft(IInventory inventory, CraftingRecipe recipe);
    }
}
