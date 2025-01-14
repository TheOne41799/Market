using InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Environment
{
    public class EnvironmentView : MonoBehaviour
    {
        [SerializeField] private List<LootItem> armourLootItems;
        [SerializeField] private List<LootItem> consumableLootItems;
        [SerializeField] private List<LootItem> treasureLootItems;
        [SerializeField] private List<LootItem> weaponLootItems;

        [SerializeField] private Transform[] spawnPositions;

        public List<GameObject> spawnedLoot = new List<GameObject>();
        private List<LootItem> lootItems = new List<LootItem>();

        public void SpawnLootItems()
        {
            for(int i = 0; i < spawnedLoot.Count; i++)
            {
                Destroy(spawnedLoot[i]);
            }

            lootItems.Clear();
            
            lootItems = new List<LootItem>
            {
            armourLootItems[Random.Range(0, armourLootItems.Count)],
            consumableLootItems[Random.Range(0, consumableLootItems.Count)],
            treasureLootItems[Random.Range(0, treasureLootItems.Count)],
            weaponLootItems[Random.Range(0, weaponLootItems.Count)]
            };

            for(int i = 0; i < spawnPositions.Length; i++)
            {
                int randomIndex = Random.Range(0, lootItems.Count);
                spawnedLoot.Add(GameObject.Instantiate(lootItems[randomIndex].gameObject, spawnPositions[i]));
            }
        }
    }
}
