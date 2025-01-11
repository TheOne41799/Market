using InventorySystem.Inputs;
using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Shop;
using InventorySystem.UI;
using UnityEngine;

namespace InventorySystem.Main
{
    public class GameService : MonoBehaviour
    {
        private InputService inputService;
        private PlayerService playerService;
        private InventoryService inventoryService;
        private ShopService shopService;
        private UIService uiService;

        [SerializeField] private PlayerView playerViewPrefab;
        [SerializeField] private InventoryView inventoryViewPrefab;
        [SerializeField] private ShopView shopViewPrefab;
        [SerializeField] private UIView uiViewPrefab;
        [SerializeField] private ItemDatabase itemDatabase;

        //todo - check for inventory full condition

        private void Awake()
        {
            inputService = new InputService();
            playerService = new PlayerService(playerViewPrefab);
            inventoryService = new InventoryService(inventoryViewPrefab, playerService);
            shopService = new ShopService(shopViewPrefab, itemDatabase, playerService);
            uiService = new UIService(uiViewPrefab, inventoryService, playerService); //this i think is not needed
        }

        private void Update()
        {
            playerService?.Update();
            inputService?.Update();
            //shopService?.Update();
        }
    }
}
