using InventorySystem.Audio;
using InventorySystem.Events;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Shop;
using InventorySystem.Slot;
using InventorySystem.UI;
using System;
using UnityEngine;
using static UnityEditor.Progress;

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

            model.CurrentInventoryWeight = 0;
            model.CurrentInventorySize = 0;

            InitializeSlots();
            ToggleInventoryUI();

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

                slot.slotType = SlotType.INVENTORY_ITEM;
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
            //currentSelectedSlot.slotType = SlotType.INVENTORY_ITEM;
        }

        private void PreviouslySelectedSlotBGColorReset()
        {
            previouslySelectedSlot = currentSelectedSlot;
            if (previouslySelectedSlot != null) previouslySelectedSlot.bgImage.color = Color.white;
        }

        public void SellItemChecks()
        {
            if (currentSelectedSlot == null || currentSelectedSlot.itemSO == null)
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UI.UIPopup.SELECT_ITEM_TO_BUY_OR_SELL);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if(currentSelectedSlot.slotType == SlotType.SHOP_ITEM)
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UI.UIPopup.SELECT_INVENTORY_ITEM_TO_SELL);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if(currentSelectedSlot.slotType == SlotType.INVENTORY_ITEM)
            {
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.CONFIRM_SELL);
                EventService.Instance.OnSlotClicked.InvokeEvent(currentSelectedSlot);
            }











            /*if (currentSelectedSlot == null || currentSelectedSlot.itemSO == null
                || currentSelectedSlot.gameObject.GetComponentInParent<ShopView>())
            {
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }*/

            /*if (currentSelectedSlot == null)
            {
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }*/










            //This is the code to sell
            /*if (currentSelectedSlot.gameObject.GetComponentInParent<InventoryView>())
            {
                EventService.Instance.OnItemSold.InvokeEvent(currentSelectedSlot.itemSO.itemSellingPrice);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(
                    Audio.AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD,
                    false);

                InventoryWeightOnItemSell(currentSelectedSlot.itemSO.itemWeight);

                currentSelectedSlot.itemSO = null;
                currentSelectedSlot.UpdateUISlot();
            }*/
        }

        public void DoSellItem(bool doSellItem)
        {
            if(doSellItem)
            {
                SellItem();
            }
            Debug.Log("No");
        }

        private void SellItem()
        {
            Debug.Log("Yes");
            EventService.Instance.OnItemSold.InvokeEvent(currentSelectedSlot.itemSO.itemSellingPrice);
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(
                Audio.AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD,
                false);
            EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.ITEM_SOLD);

            InventoryWeightOnItemSell(currentSelectedSlot.itemSO.itemWeight);

            currentSelectedSlot.itemSO = null;
            currentSelectedSlot.UpdateUISlot();
        }

        public void UpdateInventory(ItemSO itemSO)
        {
            if (model.CurrentInventoryWeight + itemSO.itemWeight > model.MaxInventoryWeight
                || model.CurrentInventorySize >= model.MaxInventorySize)
            {
                return;
            }

            AddItem(itemSO, itemSO.quantity);

            InventoryWeightOnPurchase(itemSO.itemWeight);
        }

        private void InventoryWeightOnPurchase(int weight)
        {
            model.CurrentInventoryWeight += weight;
            model.CurrentInventorySize++;
            //Debug.Log(model.InventoryWeight);

            EventService.Instance.UpdateUI.InvokeEvent();
        }

        private void InventoryWeightOnItemSell(int weight)
        {
            model.CurrentInventoryWeight -= weight;
            model.CurrentInventorySize--;
            //Debug.Log(model.InventoryWeight);
            EventService.Instance.UpdateUI.InvokeEvent();
        }

        /*public void InventoryOnItemPickUp(ItemSO itemSO, int quantity)
        {
            if (model.CurrentInventoryWeight + itemSO.itemWeight > model.MaxInventoryWeight 
                || model.CurrentInventorySize >= model.MaxInventorySize)
            {
                Debug.Log("Inventory cant carry anymore weight");
                return;
            }
            AddItem(itemSO, itemSO.quantity);
            InventoryWeightOnPurchase(itemSO.itemWeight);

            model.CurrentInventoryWeight += itemSO.itemWeight;
            model.CurrentInventorySize++;
        }*/

        public int GetInventorySize()
        {
            return model.CurrentInventorySize;
        }

        public int GetInventoryWeight()
        {
            return model.CurrentInventoryWeight;
        }

        public int GetMaxInventorySize()
        {
            return model.MaxInventorySize;
        }

        public int GetMaxInventoryWeight()
        {
            return model.MaxInventoryWeight;
        }
    }
}
