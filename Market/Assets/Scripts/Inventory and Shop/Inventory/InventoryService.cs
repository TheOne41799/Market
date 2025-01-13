using InventorySystem.Events;
using UnityEngine;

namespace InventorySystem.Inventory
{
    public class InventoryService
    {
        public InventoryController InventoryController { get; }

        public InventoryService(InventoryView inventoryViewPrefab) 
        { 
            var inventoryModel = new InventoryModel();
            var inventoryiew = GameObject.Instantiate(inventoryViewPrefab);

            InventoryController = new InventoryController(inventoryModel, inventoryiew);

            EventService.Instance.OnInventoryToggle.AddListener(InventoryController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.AddListener(InventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.AddListener(InventoryController.UpdateInventory);
            EventService.Instance.OnConfirmSell.AddListener(InventoryController.DoSellItem);
        }

        ~InventoryService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(InventoryController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.RemoveListener(InventoryController.CurrentSelectedSlot);
            EventService.Instance.OnInventoryUpdate.RemoveListener(InventoryController.UpdateInventory);
            EventService.Instance.OnConfirmSell.RemoveListener(InventoryController .DoSellItem);
        }
    }
}