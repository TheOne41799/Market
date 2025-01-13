using InventorySystem.Events;
using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Shop
{
    public class ShopService
    {
        private ShopController shopController;
        private PlayerService playerService;
        private InventoryService inventoryService;

        public ShopService(ShopView shopViewPrefab, ItemDatabase database, PlayerService service, InventoryService invService) 
        {
            var shopView = GameObject.Instantiate(shopViewPrefab);

            this.playerService = service;
            this.inventoryService = invService;

            shopController = new ShopController(shopView, database, playerService, inventoryService);

            EventService.Instance.OnInventoryToggle.AddListener(shopController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.AddListener(shopController.CurrentSelectedSlot);
            EventService.Instance.OnConfirmBuy.AddListener(shopController.DoBuyItem);
        }

        ~ShopService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(shopController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.RemoveListener(shopController.CurrentSelectedSlot);
            EventService.Instance.OnConfirmBuy.RemoveListener(shopController.DoBuyItem);
        }
    }
}
