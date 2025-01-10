using UnityEngine;

namespace InventorySystem.Player
{
    public class PlayerView : MonoBehaviour
    {
        public void Move(Vector3 movement)
        {
            transform.Translate(movement, Space.World);
        }
    }
}