using InventorySystem.Inputs;
using InventorySystem.Player;
using UnityEngine;

namespace InventorySystem.Main
{
    public class GameService : MonoBehaviour
    {
        private InputService inputService;
        private PlayerService playerService;

        [SerializeField] private PlayerView playerViewPrefab;

        private void Awake()
        {
            inputService = new InputService();
            playerService = new PlayerService(playerViewPrefab);
        }

        private void Update()
        {
            playerService?.Update();
            inputService?.Update();
        }
    }
}
