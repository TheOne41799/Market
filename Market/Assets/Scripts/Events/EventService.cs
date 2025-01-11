using InventorySystem.Items;
using InventorySystem.Slot;
using UnityEngine;

namespace InventorySystem.Events
{
    public class EventService
    {
        private static EventService instance;
        public static EventService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        public GameEventController OnInventoryToggle { get; }
        public GameEventController<Vector3> OnPlayerMove { get; }
        public GameEventController<ItemSO, int> OnItemLooted { get; }
        public GameEventController<SlotView> OnSlotClicked { get; }
        public GameEventController<int> OnItemPurchased { get; }

        public GameEventController<int> OnItemSold { get; }
        public GameEventController<ItemSO> OnInventoryUpdate { get; }


        public EventService()
        {
            OnInventoryToggle = new GameEventController();
            OnPlayerMove = new GameEventController<Vector3>();
            OnItemLooted = new GameEventController<ItemSO, int>();
            OnSlotClicked = new GameEventController<SlotView>();
            OnItemPurchased = new GameEventController<int>();
            OnInventoryUpdate = new GameEventController<ItemSO>();
            OnItemSold = new GameEventController<int>();
        }
    }
}