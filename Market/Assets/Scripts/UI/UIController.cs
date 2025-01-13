using InventorySystem.Inventory;
using InventorySystem.Player;
using InventorySystem.Slot;

namespace InventorySystem.UI
{
    public class UIController
    {
        private UIView view;
        public InventoryController inventoryController {  get; private set; }
        public PlayerService playerService { get; private set; }

        public UIController(UIView view, InventoryService service, PlayerService playerService)
        {
            this.view = view;
            this.playerService = playerService;

            view.SetUIController(this);

            inventoryController = service.InventoryController;
        }

        public void UpdateUI()
        {
            view.UpdatePlayerMoneyText();
            view.UpdatePlayerInventorySizeText();
            view.UpdatePlayerInventoryWeightText();
        }

        public void UpdateTooltipUI(SlotView slotView)
        {
            view.UpdateItemTooltipUI(slotView.itemSO, slotView.slotType);
        }

        public void ToggleInventoryUI()
        {
            view.ToggleInventoryUI();
        }

        public void HandleUIPopups(UIPopup uIPopup)
        {
            view.HandlePopups(uIPopup);
        }
    }
}
