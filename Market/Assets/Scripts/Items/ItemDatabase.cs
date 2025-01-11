using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Database", menuName = "Shop/ New Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemSO> materialItems;
        public List<ItemSO> weaponItems;
        public List<ItemSO> consumableItems;
        public List<ItemSO> treasureItems;
    }
}