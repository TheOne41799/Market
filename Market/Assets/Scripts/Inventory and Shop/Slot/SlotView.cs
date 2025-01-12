using InventorySystem.Events;
using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.Slot
{
    public class SlotView : MonoBehaviour, IPointerClickHandler
    {
        public ItemSO itemSO;
        public int quantity;
        public Image bgImage;

        [SerializeField] private Image itemIcon;        
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Image quantityTextBGImage;

        public void UpdateUISlot()
        {
            if (itemSO != null)
            {
                itemIcon.sprite = itemSO.itemIcon;
                itemIcon.gameObject.SetActive(true);
                quantityText.text = quantity.ToString();
                quantityTextBGImage.gameObject.SetActive(true);
                //Debug.Log("asdsd");
            }
            else
            {
                itemIcon.gameObject.SetActive(false);
                quantityText.text = "";
                quantityTextBGImage.gameObject.SetActive(false);
                //Debug.Log("asdsd");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                EventService.Instance.OnSlotClicked.InvokeEvent(this);

                EventService.Instance.OnAudioEffectPlay.InvokeEvent(Audio.AudioTypes.INVENTORY_ITEM_SELECTED, false);
            }
        }
    }
}
