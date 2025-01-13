using InventorySystem.Events;
using UnityEngine;

namespace InventorySystem.Inputs
{
    public class InputService
    {
        public void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontal, 0, vertical);

            if (movement.magnitude > 0)
            {
                EventService.Instance.OnPlayerMove.InvokeEvent(movement);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                EventService.Instance.OnInventoryToggle.InvokeEvent();
            }
        }
    }
}


