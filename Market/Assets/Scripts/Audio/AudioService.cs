using InventorySystem.Events;

namespace InventorySystem.Audio
{
    public class AudioService
    {
        public AudioController audioController {  get; private set; }

        public AudioService(AudioView audioViewPrefab, AudioDatabaseSO database)
        {
            audioController = new AudioController(audioViewPrefab, database);

            EventService.Instance.OnBackgroundMusicPlay.AddListener(audioController.PlaybackgroundMusic);
            EventService.Instance.OnAudioEffectPlay.AddListener(audioController.PlaySoundEffects);
        }
    }
}
