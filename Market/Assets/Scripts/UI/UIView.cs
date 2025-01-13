using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventorySystem.Player;
using InventorySystem.Items;
using UnityEngine.UI;
using InventorySystem.Slot;
using InventorySystem.Events;
using InventorySystem.Audio;

namespace InventorySystem.UI
{
    public class UIView : MonoBehaviour
    {
        private UIController uiController;

        [SerializeField] private TextMeshProUGUI playerMoneyText;
        [SerializeField] private TextMeshProUGUI playerInventorySizeText;
        [SerializeField] private TextMeshProUGUI playerInventoryWeightText;

        [SerializeField] private GameObject itemTooltipGO;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemPurchaseOrSellText;
        [SerializeField] private TextMeshProUGUI itemPurchaseOrSellPrice;
        [SerializeField] private TextMeshProUGUI itemWeight;

        [Header("UI Popups")]
        [SerializeField] private GameObject uiPopupSelectAnItem;
        [SerializeField] private Button uiPopupSelectAnItemCloseButton;

        [SerializeField] private GameObject uiPopupSelectInventoryItemToSell;
        [SerializeField] private Button uiPopupSelectInventoryItemToSellCloseButton;

        [SerializeField] private GameObject uiPopupSelectShopItemToBuy;
        [SerializeField] private Button uiPopupSelectShopItemToBuyCloseButton;

        [SerializeField] private GameObject uiPopupInventorySizeOverflow;
        [SerializeField] private Button uiPopupInventorySizeOverflowCloseButton;

        [SerializeField] private GameObject uiPopupInventoryWeightOverflow;
        [SerializeField] private Button uiPopupInventoryWeightOverflowCloseButton;

        [SerializeField] private GameObject uiPopupNotEnoughMoney;
        [SerializeField] private Button uiPopupNotEnoughMoneyCloseButton;

        [SerializeField] private GameObject uiPopupInventoryConfirmBuy;
        [SerializeField] private Button uiPopupInventoryConfirmBuyYesButton;
        [SerializeField] private Button uiPopupInventoryConfirmBuyNoButton;
        [SerializeField] private TextMeshProUGUI uiPopupInventoryConfirmBuyText;
        [SerializeField] private TextMeshProUGUI uiPopInventoryBuyItemDetailsText;

        [SerializeField] private GameObject uiPopupItemPurchased;
        [SerializeField] private TextMeshProUGUI uiPopupItemPurchasedText;
        [SerializeField] private Button uiPopupItemPurchasedCloseButton;

        [SerializeField] private GameObject uiPopupInventoryConfirmSell;
        [SerializeField] private Button uiPopupInventoryConfirmSellYesButton;
        [SerializeField] private Button uiPopupInventoryConfirmSellNoButton;
        [SerializeField] private TextMeshProUGUI uiPopupInventoryConfirmSellText;
        [SerializeField] private TextMeshProUGUI uiPopInventorySellItemDetailsText;

        [SerializeField] private GameObject uiPopupItemSold;
        [SerializeField] private TextMeshProUGUI uiPopupItemSoldText;
        [SerializeField] private Button uiPopupItemSoldCloseButton;



        private int playerMoney;
        private int playerInventorySize;
        private int playerInventoryWeight;


        private void Start()
        {
            UpdatePlayerMoneyText();
            UpdatePlayerInventorySizeText();
            UpdatePlayerInventoryWeightText();

            ToggleInventoryUI();
            ConnectUIPopupButtons();
        }

        private void ConnectUIPopupButtons()
        {
            uiPopupSelectAnItemCloseButton.onClick.AddListener(UIPopupSelectAnItemClose);
            uiPopupSelectInventoryItemToSellCloseButton.onClick.AddListener(UIPopupSelectInventoryItemToSellClose);
            uiPopupSelectShopItemToBuyCloseButton.onClick.AddListener(UIPopupSelectShopItemToBuyClose);
            uiPopupInventorySizeOverflowCloseButton.onClick.AddListener(UIPopupInventorySizeOverflowClose);
            uiPopupInventoryWeightOverflowCloseButton.onClick.AddListener(UIPopupInventoryWeightOverflowClose);
            uiPopupNotEnoughMoneyCloseButton.onClick.AddListener(UIPopupNotEnoughMoneyClose);

            uiPopupInventoryConfirmBuyYesButton.onClick.AddListener(UIPopupConfirmBuyYes);
            uiPopupInventoryConfirmBuyNoButton.onClick.AddListener(UIPopupConfirmBuyNo);
            uiPopupItemPurchasedCloseButton.onClick.AddListener(UIPopupItemPurchasedClose);

            uiPopupInventoryConfirmSellYesButton.onClick.AddListener(UIPopupConfirmSellYes);
            uiPopupInventoryConfirmSellNoButton.onClick.AddListener(UIPopupConfirmSellNo);
            uiPopupItemSoldCloseButton.onClick.AddListener(UIPopupItemSoldClose);
        }

        public void SetUIController(UIController controller)
        {
            uiController = controller;
        }

        public void UpdatePlayerMoneyText()
        {
            playerMoney = uiController.playerService.GetPlayerMoney();

            playerMoneyText.text = "PlayerMoney: " + playerMoney.ToString();
        }

        public void UpdatePlayerInventorySizeText()
        {
            playerInventorySize = uiController.inventoryController.GetInventorySize();
            //Debug.Log(uiController.inventoryController.GetInventorySize());

            playerInventorySizeText.text = "InventorySize: " + playerInventorySize.ToString() + " / " +
                                            uiController.inventoryController.GetMaxInventorySize().ToString();
        }

        public void UpdatePlayerInventoryWeightText()
        {
            int playerInventoryWeight = uiController.inventoryController.GetInventoryWeight();
            //Debug.Log(uiController.inventoryController.GetInventoryWeight());

            playerInventoryWeightText.text = "InventoryWeight: " + playerInventoryWeight.ToString() + "/ " + 
                                              uiController.inventoryController.GetMaxInventoryWeight().ToString();
        }

        public void UpdateItemTooltipUI(ItemSO itemSo, SlotType slotType)
        {
            if (itemSo != null)
            {
                itemName.text = itemSo.itemName;
                itemIcon.sprite = itemSo.itemIcon;
                itemWeight.text = itemSo.itemWeight.ToString();

                switch(slotType)
                {
                    case SlotType.INVENTORY_ITEM:
                        itemPurchaseOrSellText.text = "SELL";
                        itemPurchaseOrSellPrice.text = itemSo.itemSellingPrice.ToString();
                        break;
                    case SlotType.SHOP_ITEM:
                        itemPurchaseOrSellText.text = "BUY";
                        itemPurchaseOrSellPrice.text = itemSo.itemPurchasingPrice.ToString();
                        break;
                }
            }
        }

        public void ToggleInventoryUI()
        {
            itemTooltipGO.SetActive(!itemTooltipGO.activeSelf);
        }

        public void HandlePopups(UIPopup uIPopup)
        {
            switch(uIPopup)
            {
                case UIPopup.SELECT_ITEM_TO_BUY_OR_SELL:
                    uiPopupSelectAnItem.SetActive(true);
                    break;
                case UIPopup.SELECT_INVENTORY_ITEM_TO_SELL:
                    uiPopupSelectInventoryItemToSell.SetActive(true);
                    break;
                case UIPopup.SELECT_SHOP_ITEM_TO_BUY:
                    uiPopupSelectShopItemToBuy.SetActive(true);
                    break;
                case UIPopup.INVENTORY_SIZE_OVERFLOW:
                    uiPopupInventorySizeOverflow.SetActive(true);
                    break;
                case UIPopup.INVENTORY_WEIGHT_OVERFLOW:
                    uiPopupInventoryWeightOverflow.SetActive(true);
                    break;
                case UIPopup.NOT_ENOUGH_MONEY:
                    uiPopupNotEnoughMoney.SetActive(true);
                    break;
                case UIPopup.CONFIRM_BUY:
                    uiPopupInventoryConfirmBuy.SetActive(true);
                    ConfirmBuyPopupTextDetails(itemName.text.ToString(), itemPurchaseOrSellPrice.text.ToString());
                    break;
                case UIPopup.CONFIRM_SELL:
                    uiPopupInventoryConfirmSell.SetActive(true);
                    ConfirmSellPopupTextDetails(itemName.text.ToString(), itemPurchaseOrSellPrice.text.ToString());
                    break;
                case UIPopup.ITEM_PURCHASED:
                    uiPopupItemPurchased.SetActive(true);
                    UIPopupItemPurchased(itemName.text.ToString(), itemPurchaseOrSellPrice.text.ToString());
                    break;
                case UIPopup.ITEM_SOLD:
                    uiPopupItemSold.SetActive(true);
                    UIPopupItemSold(itemName.text.ToString(), itemPurchaseOrSellPrice.text.ToString());
                    break;
                
            }
        }

        private void UIPopupSelectAnItemClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupSelectAnItem.SetActive(false);
        }

        private void UIPopupSelectInventoryItemToSellClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupSelectInventoryItemToSell.SetActive(false);
        }

        private void UIPopupSelectShopItemToBuyClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupSelectShopItemToBuy.SetActive(false);
        }

        private void UIPopupInventorySizeOverflowClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupInventorySizeOverflow.SetActive(false);
        }

        private void UIPopupInventoryWeightOverflowClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupInventoryWeightOverflow.SetActive(false);
        }

        private void UIPopupNotEnoughMoneyClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupNotEnoughMoney.SetActive(false);
        }

        private void UIPopupConfirmBuyYes()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);

            //Debug.Log("Yes");
            EventService.Instance.OnConfirmBuy.InvokeEvent(true);
            uiPopupInventoryConfirmBuy.SetActive(false);
        }

        private void UIPopupConfirmBuyNo()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);

            //Debug.Log("No");
            EventService.Instance.OnConfirmBuy.InvokeEvent(false);
            uiPopupInventoryConfirmBuy.SetActive(false);
        }

        private void ConfirmBuyPopupTextDetails(string itemName, string itemBuyPrice)
        {
            uiPopupInventoryConfirmBuyText.text = "Do you want to buy the following item?";
            uiPopInventoryBuyItemDetailsText.text = "'" + itemName + "' for a price of " + itemBuyPrice;

            /*uiPopupItemPurchased.SetActive(true);
            UIPopupItemPurchased(itemName, itemBuyPrice);*/
        }

        private void UIPopupItemPurchased(string itemName, string itemBuyPrice)
        {
            uiPopupItemPurchasedText.text = "Purchased '" + itemName + "' for a price of " + itemBuyPrice;
        }

        private void UIPopupItemPurchasedClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupItemPurchased.SetActive(false);
        }





        private void UIPopupConfirmSellYes()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);

            Debug.Log("Yes");
            EventService.Instance.OnConfirmSell.InvokeEvent(true);
            uiPopupInventoryConfirmSell.SetActive(false);
        }

        private void UIPopupConfirmSellNo()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);

            Debug.Log("No");
            EventService.Instance.OnConfirmSell.InvokeEvent(false);
            uiPopupInventoryConfirmSell.SetActive(false);
        }

        private void ConfirmSellPopupTextDetails(string itemName, string itemSellPrice)
        {
            uiPopupInventoryConfirmSellText.text = "Do you want to sell the following item?";
            uiPopInventorySellItemDetailsText.text = "'" + itemName + "' for a price of " + itemSellPrice;
        }

        private void UIPopupItemSold(string itemName, string itemBuyPrice)
        {
            uiPopupItemSoldText.text = "Sold '" + itemName + "' for a price of" + itemBuyPrice;
        }

        private void UIPopupItemSoldClose()
        {
            EventService.Instance.OnAudioEffectPlay.InvokeEvent(AudioTypes.INVENTORY_ITEM_PURCHASED_AND_SOLD, false);
            uiPopupItemSold.SetActive(false);
        }
    }
}
