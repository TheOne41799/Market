using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventorySystem.Player;
using InventorySystem.Items;
using UnityEngine.UI;
using InventorySystem.Slot;

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

        [SerializeField] private GameObject UIPopups;
        [SerializeField] private List<GameObject> UIPopupsList;

        private int playerMoney;
        private int playerInventorySize;
        private int playerInventoryWeight;


        private void Start()
        {
            UpdatePlayerMoneyText();
            UpdatePlayerInventorySizeText();
            UpdatePlayerInventoryWeightText();

            ToggleInventoryUI();
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
                case UIPopup.INVENTORY_SIZE_OVERFLOW:
                    break;
                case UIPopup.INVENTORY_WEIGHT_OVERFLOW:
                    break;
                case UIPopup.NOT_ENOUGH_MONEY:
                    break;
                case UIPopup.CONFIRM_BUY_SELL:
                    break;
                case UIPopup.ITEM_PURCHASED_SOLD:
                    break;
            }
        }
    }
}
