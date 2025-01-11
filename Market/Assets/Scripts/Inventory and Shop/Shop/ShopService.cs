using InventorySystem.Events;
using InventorySystem.Inventory;
using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Shop
{
    public class ShopService
    {
        private ShopController shopController;

        public ShopService(ShopView shopViewPrefab, ItemDatabase database) 
        {
            var ShopModel = new ShopModel();
            var shopView = GameObject.Instantiate(shopViewPrefab);

            shopController = new ShopController(ShopModel, shopView, database);

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
