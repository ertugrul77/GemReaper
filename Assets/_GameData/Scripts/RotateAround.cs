using UnityEngine;

namespace _GameData.Scripts
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] private MoveAxis moveAxis;
        [SerializeField] private float speed;

        private Vector3 _direction;
        void Start()
        {
            _direction = moveAxis switch
            {
                MoveAxis.Left => Vector3.up,
                MoveAxis.Up => Vector3.right,
                _ => Vector3.forward
            };
        }

        private void Update()
        {
            transform.RotateAround(_direction, speed);
        }
    }

    public enum MoveAxis
    {
        Left,
        Up,
        Forward
    }
}