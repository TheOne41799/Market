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

        [SerializeField] private TextMeshProUGUI itemName; //on top
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemPurchaseOrSellText; //below the icon
        [SerializeField] private TextMeshProUGUI itemPurchaseOrSellPrice;
        [SerializeField] private TextMeshProUGUI itemWeight;

        private int playerMoney;
        private int playerInventorySize;
        private int playerInventoryWeight;


        private void Start()
        {
            UpdatePlayerMoneyText();
            UpdatePlayerInventorySizeText();
            UpdatePlayerInventoryWeightText();
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
    }
}
