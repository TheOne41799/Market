using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/ New Item")]
    public class ItemSO : ScriptableObject
    {
        public string itemName;

        [TextArea(4, 4)]
        public string itemDescription;

        public Sprite itemIcon;

        [Header("Prices")]
        public int itemSellingPrice;
        public int itemPurchasingPrice;

        [Header("Qualities")]
        public int itemWeight;
        public ItemType itemType;
        public int quantity;
        public ItemRarity itemRarity;
    }
}