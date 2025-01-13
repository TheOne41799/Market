using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Database", menuName = "Shop/ New Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemSO> armourAndShieldItems;
        public List<ItemSO> weaponItems;
        public List<ItemSO> consumableItems;
        public List<ItemSO> treasureItems;
    }
}