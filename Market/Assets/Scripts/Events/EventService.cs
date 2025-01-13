using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Slot;
using InventorySystem.Audio;
using UnityEngine;
using InventorySystem.UI;

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
        //public GameEventController<ItemSO> OnItemLooted { get; }
        public GameEventController<SlotView> OnSlotClicked { get; }
        public GameEventController<int> OnItemPurchased { get; }
        public GameEventController<int> OnItemSold { get; }
        public GameEventController<ItemSO> OnInventoryUpdate { get; }
        public GameEventController UpdateUI { get; }
        public GameEventController<AudioTypes, bool> OnBackgroundMusicPlay { get; }
        public GameEventController<AudioTypes, bool> OnAudioEffectPlay { get; }
        public GameEventController<UIPopup> OnUIPopup { get; }


        public EventService()
        {
            OnInventoryToggle = new GameEventController();
            OnPlayerMove = new GameEventController<Vector3>();
            //OnItemLooted = new GameEventController<ItemSO, int>();
            OnSlotClicked = new GameEventController<SlotView>();
            OnItemPurchased = new GameEventController<int>();
            OnInventoryUpdate = new GameEventController<ItemSO>();
            OnItemSold = new GameEventController<int>();
            UpdateUI = new GameEventController();
            OnBackgroundMusicPlay = new GameEventController<AudioTypes, bool>();
            OnAudioEffectPlay = new GameEventController<AudioTypes, bool>();
            OnUIPopup = new GameEventController<UIPopup>();
        }
    }
}