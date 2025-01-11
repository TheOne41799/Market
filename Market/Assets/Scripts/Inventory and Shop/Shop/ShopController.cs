using InventorySystem.Events;
using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Slot;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Shop
{
    public class ShopController
    {
        private ShopModel model;
        private ShopView view;
        private ItemDatabase itemDatabase;

        private PlayerService playerService;
        private InventoryService inventoryService;

        private SlotView currentSelectedSlot;
        private SlotView previouslySelectedSlot;

        public ShopController(ShopModel model, ShopView view, ItemDatabase database, PlayerService service, InventoryService inventoryService)
        {
            this.model = model;
            this.view = view;
            this.itemDatabase = database;
            this.playerService = service;
            this.inventoryService = inventoryService;

            InitializeSlots();
            ToggleInventoryUI();

            view.SetShopController(this);
            
        }

        public void Update()
        {

        }

        public void ToggleInventoryUI()
        {
            view.gameObject.SetActive(!view.gameObject.activeSelf);
        }

        private void InitializeSlots()
        {
            for (int i = 0; i < view.materialItemSlots.Length && i < itemDatabase.materialItems.Count; i++)
            {
                view.materialItemSlots[i].itemSO = itemDatabase.materialItems[i];
                view.materialItemSlots[i].quantity = itemDatabase.materialItems[i].quantity;

                view.materialItemSlots[i].UpdateUISlot();
            }

            /*previouslySelectedSlot = null;
            currentSelectedSlot = null;*/

            //after implementing player moeny and inventory weight - add remaining database items
        }

        public void CurrentSelectedSlot(SlotView slotView)
        {
            PreviouslySelectedSlotBGColorReset();

            currentSelectedSlot = slotView;
            currentSelectedSlot.bgImage.color = Color.red;
        }

        private void PreviouslySelectedSlotBGColorReset()
        {
            previouslySelectedSlot = currentSelectedSlot;
            if (previouslySelectedSlot != null) previouslySelectedSlot.bgImage.color = Color.white;
        }

        public void PurchaseItem()
        {
            if (currentSelectedSlot == null) return;
            if (currentSelectedSlot.itemSO == null) return;

            if(currentSelectedSlot.itemSO.itemPurchasingPrice > playerService.GetPlayerMoney()) return;

            if (inventoryService.InventoryController.GetInventoryWeight() >= 100) return;
            if (inventoryService.InventoryController.GetInventorySize() >= 12) return;

            if (currentSelectedSlot.gameObject.GetComponentInParent<ShopView>())
            {
                EventService.Instance.OnItemPurchased.InvokeEvent(currentSelectedSlot.itemSO.itemPurchasingPrice);
                EventService.Instance.OnInventoryUpdate.InvokeEvent(currentSelectedSlot.itemSO);

                //currentSelectedSlot.itemSO = null;
                currentSelectedSlot.UpdateUISlot();            }
        }
    }
}
