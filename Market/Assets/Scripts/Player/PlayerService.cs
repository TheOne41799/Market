using InventorySystem.Events;
using UnityEngine;

namespace InventorySystem.Player
{
    public class PlayerService
    {
        private PlayerController playerController;

        public PlayerService(PlayerView playerViewPrefab)
        {
            var playerModel = new PlayerModel();

            var playerView = GameObject.Instantiate(playerViewPrefab);

            playerController = new PlayerController(playerModel, playerView);

            EventService.Instance.OnPlayerMove.AddListener(playerController.HandleMovement);
        }

        ~PlayerService()
        {
            EventService.Instance.OnPlayerMove.RemoveListener(playerController.HandleMovement);
        }

        public void Update()
        {
            playerController?.Update();
        }
    }
}