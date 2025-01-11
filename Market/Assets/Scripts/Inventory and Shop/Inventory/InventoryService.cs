using InventorySystem.Events;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Inventory
{
    public class InventoryService
    {
        private InventoryController inventoryController;
        private PlayerService playerService;

        public InventoryService(InventoryView inventoryViewPrefab, PlayerService service) 
        { 
            var inventoryModel = new InventoryModel();
            var inventoryiew = GameObject.Instantiate(inventoryViewPrefab);

            this.playerService = service;

            inventoryController = new InventoryController(inventoryModel, inventoryiew, playerService);

            

            EventService.Instance.OnInventoryToggle.AddListener(inventoryController.ToggleInventoryUI);
            EventService.Instance.OnItemLooted.AddListener(inventoryController.AddItem);
            EventService.Instance.OnSlotClicked.AddListener(inventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.AddListener(inventoryController.UpdateInventory);
        }

        ~InventoryService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(inventoryController.ToggleInventoryUI);
            EventService.Instance.OnItemLooted.RemoveListener(inventoryController.AddItem);
            EventService.Instance.OnSlotClicked.RemoveListener(inventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.RemoveListener(inventoryController.UpdateInventory);
        }

        public void Update()
        {
            inventoryController?.Update();
        }
    }
}