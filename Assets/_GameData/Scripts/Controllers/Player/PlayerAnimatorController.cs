using UnityEngine;

namespace _GameData.Scripts.Controllers.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private static readonly int MovementAlpha = Animator.StringToHash("MovementAlpha");
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            _animator.SetFloat(MovementAlpha, FloatingJoystick.Instance.Direction.magnitude);
        }
    }
}
