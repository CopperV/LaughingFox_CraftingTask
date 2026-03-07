using UnityEngine;

namespace CopGameDev.LaughingFoxTest.Crafting
{
    public class CraftingHandler : MonoBehaviour
    {
        private ICraftingService craftngService = new DefaultCraftingService();

        public ICraftingService CraftngService => craftngService;
    }
}
