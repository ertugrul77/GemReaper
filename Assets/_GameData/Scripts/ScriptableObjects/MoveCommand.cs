using UnityEngine;

namespace _GameData.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "MoveCommand", fileName = "MoveCommand", order = 0)]
    public class MoveCommand : ScriptableObject
    {
        [SerializeField] private float moveOffset;
        [SerializeField] private PositionAlternator.MoveAxis moveAxis;

        public float MoveOffset => moveOffset;
        public PositionAlternator.MoveAxis MoveAxis => moveAxis;
    }
}
