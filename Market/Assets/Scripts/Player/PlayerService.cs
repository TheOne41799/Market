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
            EventService.Instance.OnItemPurchased.AddListener(playerController.DeductPlayerMoney);
            EventService.Instance.OnItemSold.AddListener(playerController.AddPlayerMoney);
        }

        ~PlayerService()
        {
            EventService.Instance.OnPlayerMove.RemoveListener(playerController.HandleMovement);
            EventService.Instance.OnItemPurchased.RemoveListener(playerController.DeductPlayerMoney);
            EventService.Instance.OnItemSold.RemoveListener(playerController.AddPlayerMoney);
        }

        public void Update()
        {
            playerController?.Update();
        }

        public int GetPlayerMoney()
        {
            return playerController.GetPlayerMoney(); 
        }
    }
}