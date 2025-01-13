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
        [SerializeField] private Button uiPopupSelectInventoryItemToSellButton;

        [SerializeField] private GameObject uiPopupSelectShopItemToBuy;
        [SerializeField] private Button uiPopupSelectShopItemToBuyButton;

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
            uiPopupSelectInventoryItemToSellButton.onClick.AddListener(UIPopupSelectInventoryItemToSellClose);
            uiPopupSelectShopItemToBuyButton.onClick.AddListener(UIPopupSelectShopItemToBuyClose);
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
                    Debug.Log("1");
                    break;
                case UIPopup.INVENTORY_WEIGHT_OVERFLOW:
                    Debug.Log("2");
                    break;
                case UIPopup.NOT_ENOUGH_MONEY:
                    Debug.Log("3");
                    break;
                case UIPopup.CONFIRM_BUY_SELL:
                    Debug.Log("4");
                    break;
                case UIPopup.ITEM_PURCHASED_SOLD:
                    Debug.Log("5");
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
    }
}
