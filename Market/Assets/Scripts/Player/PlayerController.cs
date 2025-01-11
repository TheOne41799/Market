using InventorySystem.Events;
using UnityEngine;

namespace InventorySystem.Player
{
    public class PlayerController
    {
        private PlayerModel model;
        private PlayerView view;

        public PlayerController(PlayerModel model, PlayerView view)
        {
            this.model = model;
            this.view = view;
        }

        public void HandleMovement(Vector3 movement)
        {
            view.Move(movement * model.Speed * Time.deltaTime);
        }

        public void Update()
        {

        }

        public int GetPlayerMoney()
        {
            return model.PlayerMoney;
        }

        public void DeductPlayerMoney(int price)
        {
            model.PlayerMoney -= price;
            //Debug.Log(model.PlayerMoney);

            EventService.Instance.UpdateUI.InvokeEvent();
        }

        public void AddPlayerMoney(int price)
        {
            model.PlayerMoney += price;
            //Debug.Log(model.PlayerMoney);

            EventService.Instance.UpdateUI.InvokeEvent();
        }
    }
}