using System.Collections.Generic;
using UnityEngine;

namespace _GameData.Scripts.ScriptableObjects.Audio
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio Data/New AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        public AudioGroup buttonPress;
        public AudioGroup gemPing;
        public AudioGroup getMoney;

    }
}