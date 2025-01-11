using InventorySystem.Events;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Inventory
{
    public class InventoryService
    {
        public InventoryController InventoryController { get; }
        private PlayerService playerService;

        public InventoryService(InventoryView inventoryViewPrefab, PlayerService service) 
        { 
            var inventoryModel = new InventoryModel();
            var inventoryiew = GameObject.Instantiate(inventoryViewPrefab);

            this.playerService = service;

            InventoryController = new InventoryController(inventoryModel, inventoryiew, playerService);

            

            EventService.Instance.OnInventoryToggle.AddListener(InventoryController.ToggleInventoryUI);
            //EventService.Instance.OnItemLooted.AddListener(InventoryController.AddItem);

            //EventService.Instance.OnItemLooted.AddListener(inventoryController.InventoryOnItemPickUp);

            EventService.Instance.OnSlotClicked.AddListener(InventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.AddListener(InventoryController.UpdateInventory);
        }

        ~InventoryService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(InventoryController.ToggleInventoryUI);
            //EventService.Instance.OnItemLooted.RemoveListener(InventoryController.AddItem);
            EventService.Instance.OnSlotClicked.RemoveListener(InventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.RemoveListener(InventoryController.UpdateInventory);
        }

        public void Update()
        {
            InventoryController?.Update();
        }
    }
}