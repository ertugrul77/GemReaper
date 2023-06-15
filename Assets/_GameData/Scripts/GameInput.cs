using UnityEngine;

namespace _GameData.Scripts
{
    public class GameInput : MonoBehaviour
    {
        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = new Vector2(FloatingJoystick.Instance.Horizontal, FloatingJoystick.Instance.Vertical);
            return inputVector;
        }
    }
}
