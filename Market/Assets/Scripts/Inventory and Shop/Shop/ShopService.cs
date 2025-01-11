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

        public ShopService(ShopView shopViewPrefab, ItemDatabase database, PlayerService service) 
        {
            var ShopModel = new ShopModel();
            var shopView = GameObject.Instantiate(shopViewPrefab);

            this.playerService = service;

            shopController = new ShopController(ShopModel, shopView, database, playerService);

            EventService.Instance.OnInventoryToggle.AddListener(shopController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.AddListener(shopController.CurrentSelectedSlot);
        }

        ~ShopService()
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(shopController.ToggleInventoryUI);
            EventService.Instance.OnSlotClicked.RemoveListener(shopController.CurrentSelectedSlot);
        }

        public void Update()
        {
            shopController?.Update();
        }
    }
}
