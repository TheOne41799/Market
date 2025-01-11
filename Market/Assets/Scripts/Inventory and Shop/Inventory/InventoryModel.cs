using InventorySystem.Slot;

namespace InventorySystem.Inventory
{
    public class InventoryModel
    {
        public int MaxInventoryWeight { get; } = 100;
        public int CurrentInventoryWeight { get; set; } = 0;
        public int MaxInventorySize { get; set; } = 12;
        public int CurrentInventorySize { get; set;} = 0;
    }
}