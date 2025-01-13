using InventorySystem.Events;
using InventorySystem.Inventory;
using InventorySystem.Player;
using InventorySystem.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.UI
{
    public class UIService
    {
        private UIController uiController;
        private InventoryService inventoryService;
        private PlayerService playerService;

        public UIService(UIView uiViewPrefab, InventoryService service, PlayerService playerServ)
        {
            var uiModel = new UIModel();
            var uiView = GameObject.Instantiate(uiViewPrefab);

            this.inventoryService = service;
            this.playerService = playerServ;

            uiController = new UIController(uiModel, uiView, inventoryService, playerServ);

            EventService.Instance.UpdateUI.AddListener(uiController.UpdateUI);
            EventService.Instance.OnSlotClicked.AddListener(uiController.UpdateTooltipUI);
            EventService.Instance.OnInventoryToggle.AddListener(uiController.ToggleInventoryUI);
            EventService.Instance.OnUIPopup.AddListener(uiController.HandleUIPopups);
        }

        ~UIService() 
        {
            EventService.Instance.OnInventoryToggle.RemoveListener(uiController.ToggleInventoryUI);
            EventService.Instance.UpdateUI.RemoveListener(uiController.UpdateUI);
            EventService.Instance.OnSlotClicked.RemoveListener(uiController.UpdateTooltipUI);
            EventService.Instance.OnUIPopup.RemoveListener(uiController.HandleUIPopups);
        }
    }
}