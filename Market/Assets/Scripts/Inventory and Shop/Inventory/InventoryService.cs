using InventorySystem.Events;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Inventory
{
    public class InventoryService
    {
        private InventoryController inventoryController;

        public InventoryService(InventoryView inventoryViewPrefab) 
        { 
            var inventoryModel = new InventoryModel();
            var inventoryiew = GameObject.Instantiate(inventoryViewPrefab);

            inventoryController = new InventoryController(inventoryModel, inventoryiew);

            EventService.Instance.OnInventoryToggle.AddListener(inventoryController.ToggleInventoryUI);
            EventService.Instance.OnItemLooted.AddListener(inventoryController.AddItem);
            EventService.Instance.OnSlotClicked.AddListener(inventoryController.CurrentSelectedSlot);
        }

        ~InventoryService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(inventoryController.ToggleInventoryUI);
            EventService.Instance.OnItemLooted.RemoveListener(inventoryController.AddItem);
            EventService.Instance.OnSlotClicked.RemoveListener(inventoryController.CurrentSelectedSlot);
        }

        public void Update()
        {
            inventoryController?.Update();
        }
    }
}