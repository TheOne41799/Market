using InventorySystem.Events;
using InventorySystem.Items;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Items
{
    public class LootItem : MonoBehaviour
    {
        [SerializeField] private ItemSO ItemSO;
        [SerializeField] private int quantity;

        private void OnValidate()
        {
            if (ItemSO == null) return;

            this.name = ItemSO.itemName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerView>())
            {
                EventService.Instance.OnItemLooted.InvokeEvent(ItemSO, quantity);

                Destroy(this.gameObject);
            }
        }
    }
}