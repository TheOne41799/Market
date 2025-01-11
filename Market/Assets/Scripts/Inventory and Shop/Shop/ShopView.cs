using InventorySystem.Inventory;
using InventorySystem.Slot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Shop
{
    public class ShopView : MonoBehaviour
    {
        public SlotView[] armourAndShieldSlots;
        public SlotView[] weaponSlots;
        public SlotView[] consumableSlots;
        public SlotView[] treasureSlots;

        public Button purchaseButton;

        private ShopController shopController;

        private void Start()
        {
            // set script execution order
            purchaseButton.onClick.AddListener(shopController.PurchaseItem);
        }

        public void SetShopController(ShopController controller)
        {
            shopController = controller;
        }
    }
}
