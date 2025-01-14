using InventorySystem.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Environment
{
    public class EnvironmentService
    {
        private EnvironmentController environmentController;

        public EnvironmentService(EnvironmentView environmentViewPrefab)
        {
            EnvironmentView view = GameObject.Instantiate(environmentViewPrefab);

            environmentController = new EnvironmentController(view);

            EventService.Instance.OnSpawnItems.AddListener(environmentController.SpawnLootItems);
        }

        ~EnvironmentService() 
        {
            EventService.Instance.OnSpawnItems.RemoveListener(environmentController.SpawnLootItems);
        }
    }
}