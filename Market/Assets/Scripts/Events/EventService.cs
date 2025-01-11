using InventorySystem.Items;
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


        public EventService()
        {
            OnInventoryToggle = new GameEventController();
            OnPlayerMove = new GameEventController<Vector3>();
            OnItemLooted = new GameEventController<ItemSO, int>();
        }
    }
}