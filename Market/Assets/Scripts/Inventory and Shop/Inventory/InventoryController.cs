using InventorySystem.Events;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Slot;
using System;
using UnityEngine;

namespace InventorySystem.Inventory
{
    public class InventoryController
    {
        private InventoryModel model;
        private InventoryView view;

        //is dependency injection needed for player service??????????
        private PlayerService playerService;

        private SlotView currentSelectedSlot;
        private SlotView previouslySelectedSlot;

        public InventoryController(InventoryModel model, InventoryView view, PlayerService service)
        {
            this.model = model;
            this.view = view;

            this.playerService = service;

            InitializeSlots();
            //ToggleInventoryUI();

            view.SetInventoryController(this);
        }

        public void Update()
        {

        }

        public void ToggleInventoryUI()
        {
            view.gameObject.SetActive(!view.gameObject.activeSelf);
        }

        //this is not useful now - maybe useful when creating a save load system
        private void InitializeSlots()
        {
            foreach (var slot in view.itemSlots)
            {
                slot.UpdateUISlot();
            }
        }

        public void AddItem(ItemSO itemSO, int quantity)
        {
            foreach(var slot in view.itemSlots)
            {
                if(slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUISlot();
                    return;
                }                
            }
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

        public void SellItem()
        {
            if(currentSelectedSlot == null) return;            

            if (currentSelectedSlot.itemSO != null && currentSelectedSlot.gameObject.GetComponentInParent<InventoryView>())
            {
                EventService.Instance.OnItemSold.InvokeEvent(currentSelectedSlot.itemSO.itemSellingPrice);

                InventoryWeightOnItemSell(currentSelectedSlot.itemSO.itemWeight);

                currentSelectedSlot.itemSO = null;
                currentSelectedSlot.UpdateUISlot();
            }
        }

        public void UpdateInventory(ItemSO itemSO)
        {
            if (model.InventoryWeight < itemSO.itemWeight || model.CurrentInventorySize >= view.itemSlots.Length)
            {
                Debug.Log("Inventory cant carry anymore weight");
                return;
            }
            AddItem(itemSO, itemSO.quantity);
            InventoryWeightOnPurchaseOrPickup(itemSO.itemWeight);
        }

        public void InventoryWeightOnPurchaseOrPickup(int weight)
        {
            model.InventoryWeight -= weight;
            model.CurrentInventorySize++;
            //Debug.Log(model.InventoryWeight);
        }

        public void InventoryWeightOnItemSell(int weight)
        {
            model.InventoryWeight += weight;
            model.CurrentInventorySize--;
            //Debug.Log(model.InventoryWeight);
        }
    }
}
