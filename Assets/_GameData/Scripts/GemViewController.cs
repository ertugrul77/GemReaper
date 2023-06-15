using _GameData.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace _GameData.Scripts
{
    public class GemViewController : MonoBehaviour
    {
        public GemType gemType;
        public Image gemImage;
        public TextMeshProUGUI gemName;
        public TextMeshProUGUI gemCountText;
        public int gemCount;
    }
}
