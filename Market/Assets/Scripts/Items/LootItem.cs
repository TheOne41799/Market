using InventorySystem.Events;
using InventorySystem.Items;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Items
{
    public class LootItem : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private int quantity;

        private void OnValidate()
        {
            if (itemSO == null) return;

            this.name = itemSO.itemName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerView>())
            {
                
                if (itemSO == null) return;
                //EventService.Instance.OnItemLooted.InvokeEvent(ItemSO);
                Debug.Log("asdasd");
                EventService.Instance.OnInventoryUpdate.InvokeEvent(itemSO);

                Destroy(this.gameObject);
            }
        }
    }
}