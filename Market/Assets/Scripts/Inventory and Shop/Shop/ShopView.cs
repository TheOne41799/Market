using InventorySystem.Inventory;
using InventorySystem.Slot;
using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [SerializeField] private Button[] toggleButtons;
        [SerializeField] private GameObject[] itemShopGameObjects;

        [SerializeField] private TextMeshProUGUI shopUINameText;
        [SerializeField] private string[] shopUINames;

        public Button purchaseButton;

        private ShopController shopController;

        private void Start()
        {
            // set script execution order - is it needed?
            purchaseButton.onClick.AddListener(shopController.BuyItemChecks);

            for (int i = 0; i < toggleButtons.Length; i++)
            {
                int index = i;
                toggleButtons[i].onClick.AddListener(() => shopController.ToggleUI(index));

                ToggleGameObject(index);
            }
        }

        public void ToggleGameObject(int index)
        {
            for (int i = 0; i < itemShopGameObjects.Length; i++)
            {
                itemShopGameObjects[i].SetActive(i == index);                
            }

            UpdateUIName(index);
        }

        private void UpdateUIName(int index)
        {
            if (index >= 0 && index < shopUINames.Length)
            {
                shopUINameText.text = shopUINames[index];
            }
            else
            {
                shopUINameText.text = "Unknown UI";
            }
        }

        public void SetShopController(ShopController controller)
        {
            shopController = controller;
        }
    }
}
