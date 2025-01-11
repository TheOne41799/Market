using InventorySystem.Inventory;
using InventorySystem.Items;
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

        private SlotView currentSelectedSlot;
        private SlotView previouslySelectedSlot;

        public ShopController(ShopModel model, ShopView view, ItemDatabase database)
        {
            this.model = model;
            this.view = view;
            this.itemDatabase = database;

            InitializeSlots();
            //ToggleInventoryUI();

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

            if (currentSelectedSlot.itemSO != null && currentSelectedSlot.gameObject.GetComponentInParent<ShopView>())
            {
                currentSelectedSlot.itemSO = null;
                currentSelectedSlot.UpdateUISlot();
            }
        }
    }
}
