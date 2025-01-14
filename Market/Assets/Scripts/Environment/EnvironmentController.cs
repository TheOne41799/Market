using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Environment
{
    public class EnvironmentController
    {
        private EnvironmentView environmentView;

        public EnvironmentController(EnvironmentView view)
        {
            this.environmentView = view;
        }

        public void SpawnLootItems()
        {
            environmentView.SpawnLootItems();
        }
    }
}