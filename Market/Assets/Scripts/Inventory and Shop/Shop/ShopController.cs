using InventorySystem.Events;
using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Slot;
using InventorySystem.Audio;
using InventorySystem.UI;
using UnityEngine;

namespace InventorySystem.Shop
{
    public class ShopController
    {
        private ShopView view;
        private ItemDatabase itemDatabase;

        private PlayerService playerService;
        private InventoryService inventoryService;

        private SlotView currentSelectedSlot;
        private SlotView previouslySelectedSlot;

        public ShopController(ShopView view, ItemDatabase database, PlayerService service, InventoryService inventoryService)
        {
            this.view = view;
            this.itemDatabase = database;
            this.playerService = service;
            this.inventoryService = inventoryService;

            InitializeSlots();
            ToggleInventoryUI();

            view.SetShopController(this);
            
        }

        public void ToggleInventoryUI()
        {
            view.gameObject.SetActive(!view.gameObject.activeSelf);
        }

        private void InitializeSlots()
        {
            for (int i = 0; i < view.armourAndShieldSlots.Length && i < itemDatabase.armourAndShieldItems.Count; i++)
            {
                view.armourAndShieldSlots[i].itemSO = itemDatabase.armourAndShieldItems[i];
                view.armourAndShieldSlots[i].quantity = itemDatabase.armourAndShieldItems[i].quantity;

                view.armourAndShieldSlots[i].UpdateUISlot();
                view.armourAndShieldSlots[i].slotType = SlotType.SHOP_ITEM;
            }

            for (int i = 0; i < view.weaponSlots.Length && i < itemDatabase.weaponItems.Count; i++)
            {
                view.weaponSlots[i].itemSO = itemDatabase.weaponItems[i];
                view.weaponSlots[i].quantity = itemDatabase.weaponItems[i].quantity;

                view.weaponSlots[i].UpdateUISlot();
                view.weaponSlots[i].slotType = SlotType.SHOP_ITEM;
            }

            for (int i = 0; i < view.consumableSlots.Length && i < itemDatabase.consumableItems.Count; i++)
            {
                view.consumableSlots[i].itemSO = itemDatabase.consumableItems[i];
                view.consumableSlots[i].quantity = itemDatabase.consumableItems[i].quantity;

                view.consumableSlots[i].UpdateUISlot();
                view.consumableSlots[i].slotType = SlotType.SHOP_ITEM;
            }

            for (int i = 0; i < view.treasureSlots.Length && i < itemDatabase.treasureItems.Count; i++)
            {
                view.treasureSlots[i].itemSO = itemDatabase.treasureItems[i];
                view.treasureSlots[i].quantity = itemDatabase.treasureItems[i].quantity;

                view.treasureSlots[i].UpdateUISlot();
                view.treasureSlots[i].slotType = SlotType.SHOP_ITEM;
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

        public void BuyItemChecks()
        {
            if(currentSelectedSlot == null)
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.SELECT_ITEM_TO_BUY_OR_SELL);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if (currentSelectedSlot.slotType == SlotType.INVENTORY_ITEM)
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.SELECT_SHOP_ITEM_TO_BUY);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if (inventoryService.InventoryController.GetInventorySize()
               >= inventoryService.InventoryController.GetMaxInventorySize())
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.INVENTORY_SIZE_OVERFLOW);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if(inventoryService.InventoryController.GetInventoryWeight()
                   + currentSelectedSlot.itemSO.itemWeight
                   > inventoryService.InventoryController.GetMaxInventoryWeight())
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.INVENTORY_WEIGHT_OVERFLOW);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }

            if(currentSelectedSlot.itemSO.itemPurchasingPrice > playerService.GetPlayerMoney())
            {
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.NOT_ENOUGH_MONEY);
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.ERROR, false);
                return;
            }            

            if(currentSelectedSlot.slotType == SlotType.SHOP_ITEM)
            {
                EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
                EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.CONFIRM_BUY);
                EventService.Instance.OnSlotClicked.InvokeEvent(currentSelectedSlot);
            }
        }

        public void DoBuyItem(bool doBuyItem)
        {
            if(doBuyItem)
            {
                BuyItem();
            }
        }

        private void BuyItem()
        {
            EventService.Instance.OnItemPurchased.InvokeEvent(currentSelectedSlot.itemSO.itemPurchasingPrice);
            EventService.Instance.OnInventoryUpdate.InvokeEvent(currentSelectedSlot.itemSO);
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(
                Audio.AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD,
                false);
            EventService.Instance.OnUIPopup.InvokeEvent(UIPopup.ITEM_PURCHASED);

            //Needed if you need to remove the item from shop
            //currentSelectedSlot.itemSO = null;

            currentSelectedSlot.UpdateUISlot();
        }

        public void ToggleUI(int index)
        {
            view.ToggleGameObject(index);

            EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.INVENTORY_ITEM_SELECTED, false);
        }
    }
}
