using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InventorySystem.Player;
using InventorySystem.Items;

namespace InventorySystem.UI
{
    public class UIView : MonoBehaviour
    {
        private UIController uiController;

        [SerializeField] private TextMeshProUGUI playerMoneyText;
        [SerializeField] private TextMeshProUGUI playerInventorySizeText;
        [SerializeField] private TextMeshProUGUI playerInventoryWeightText;

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
    }
}
