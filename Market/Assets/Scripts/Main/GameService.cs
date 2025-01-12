using InventorySystem.Audio;
using InventorySystem.Inputs;
using InventorySystem.Inventory;
using InventorySystem.Items;
using InventorySystem.Player;
using InventorySystem.Shop;
using InventorySystem.UI;
using InventorySystem.Events;
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
        private AudioService audioService;

        [Header("Prefabs")]
        [SerializeField] private PlayerView playerViewPrefab;
        [SerializeField] private InventoryView inventoryViewPrefab;
        [SerializeField] private ShopView shopViewPrefab;
        [SerializeField] private UIView uiViewPrefab;
        [SerializeField] private AudioView audioViewPrefab;

        [Header("Database")]
        [SerializeField] private ItemDatabase itemDatabase;
        [SerializeField] private AudioDatabaseSO audioDatabase;

        //todo - check for inventory full condition

        private void Awake()
        {
            inputService = new InputService();
            playerService = new PlayerService(playerViewPrefab);
            inventoryService = new InventoryService(inventoryViewPrefab, playerService);
            shopService = new ShopService(shopViewPrefab, itemDatabase, playerService, inventoryService);
            uiService = new UIService(uiViewPrefab, inventoryService, playerService); //this i think is not needed

            audioService = new AudioService(audioViewPrefab, audioDatabase);
            Initialization();
        }

        private void Initialization()
        {
            EventService.Instance.OnBackgroundMusicPlay.InvokeEvent(AudioTypes.BACKGROUND_MUSIC, true);
        }

        private void Update()
        {
            playerService?.Update();
            inputService?.Update();
        }
    }
}
