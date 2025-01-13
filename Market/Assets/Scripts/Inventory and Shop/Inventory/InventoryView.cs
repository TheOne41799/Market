using InventorySystem.Slot;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        public SlotView[] itemSlots;
        public Button sellButton;

        private InventoryController inventoryController;


        private void Start()
        {
            // set script execution order
            sellButton.onClick.AddListener(inventoryController.SellItemChecks);
        }

        public void SetInventoryController(InventoryController controller)
        {
            inventoryController = controller;
        }
    }
}