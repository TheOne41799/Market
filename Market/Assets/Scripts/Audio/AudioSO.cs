using UnityEngine;

namespace InventorySystem.Audio
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "Audio/ New Audio SO")]
    public class AudioSO : ScriptableObject
    {
        public AudioTypes audioType;
        public AudioClip audioClip;
    }
}