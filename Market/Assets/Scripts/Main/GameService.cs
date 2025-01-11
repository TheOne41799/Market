using InventorySystem.Inputs;
using InventorySystem.Inventory;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Main
{
    public class GameService : MonoBehaviour
    {
        private InputService inputService;
        private PlayerService playerService;
        private InventoryService inventoryService;

        [SerializeField] private PlayerView playerViewPrefab;
        [SerializeField] private InventoryView inventoryViewPrefab;

        private void Awake()
        {
            inputService = new InputService();
            playerService = new PlayerService(playerViewPrefab);
            inventoryService = new InventoryService(inventoryViewPrefab);
        }

        private void Update()
        {
            playerService?.Update();
            inputService?.Update();
        }
    }
}
