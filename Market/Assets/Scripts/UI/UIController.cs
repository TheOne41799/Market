using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.UI
{
    public class UIController
    {
        private UIModel model;
        private UIView view;

        private InventoryService inventoryService;
        public InventoryController inventoryController {  get; private set; }

        public PlayerService playerService { get; private set; }   

        public UIController(UIModel model, UIView view, InventoryService service, PlayerService playerService)
        {
            this.model = model;
            this.view = view;
            this.inventoryService = service;
            this.playerService = playerService;

            view.SetUIController(this);

            inventoryController = inventoryService.inventoryController;
        }

        public void UpdateUI()
        {
            view.UpdatePlayerMoneyText();
            view.UpdatePlayerInventorySizeText();
            view.UpdatePlayerInventoryWeightText();
        }
        
        public void UpdateUIOnItemLoot(ItemSO itemSO)
        {
            view.UpdateUIOnItemLoot(itemSO);
        }
    }
}
