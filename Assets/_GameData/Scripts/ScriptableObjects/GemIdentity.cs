using UnityEngine;

namespace _GameData.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Create ItemIdentity", fileName = "ItemIdentity", order = 0)]
    public class GemIdentity : ScriptableObject
    {
        public GemType type;
        public float price;
        public Sprite iconSprite;
        public GameObject prefab;
        public ParticleSystem grownParticle;

    }
    public enum GemType
    {
        GemGreen,
        GemPink,
        GemYellow,
        GemBlue,
        GemRed
    }
}
