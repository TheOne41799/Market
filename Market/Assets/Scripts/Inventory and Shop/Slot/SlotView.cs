using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Slot
{
    public class SlotView : MonoBehaviour
    {
        public ItemSO itemSO;
        public int quantity;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI quantityText;


        public void UpdateUISlot()
        {
            if (itemSO != null)
            {
                itemIcon.sprite = itemSO.itemIcon;
                itemIcon.gameObject.SetActive(true);
                quantityText.text = quantity.ToString();
            }
            else
            {
                itemIcon.gameObject.SetActive(false);
                quantityText.text = "";
            }
        }
    }
}
