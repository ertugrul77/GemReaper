using System;
using UnityEngine;

namespace _GameData.Scripts.Controllers.Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private GameInput gameInput;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool useRigidBodyMovement;

        private Rigidbody _rb;
        private Action _movementMethod;
        private Vector3 _moveDir;
        private Vector2 _inputVector;
        private const float PlayerRadius = .4f;
        private const float PlayerHeight = 2f;
        private const float RotateSpeed = 10f;
        private const int LayerMask = 3;
        private float _moveDistance;
        private bool _canMove;

        private void Awake()
        {
            if (TryGetComponent<Rigidbody>(out _rb) && useRigidBodyMovement)
            {
                _movementMethod = HandleMovementWithRigidBody;
                moveSpeed = 1000f;
            }
            else
            {
                _movementMethod = HandleMovementWithoutRigidBody;
            }
        }
        private void Update()
        {
            _movementMethod();
        }
        private void HandleMovementWithRigidBody()
        {
            GetMoveDirections();
            _rb.velocity = _moveDir * _moveDistance;
            if (_moveDir != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, _moveDir, RotateSpeed * Time.deltaTime);
            }
        }
        private void HandleMovementWithoutRigidBody()
        {
            GetMoveDirections();
            
            _canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, _moveDir, _moveDistance, LayerMask);

            if (!_canMove)
            {
                // Cannot move towards moveDir
                // Attempt only X movement
                Vector3 moveDirX = new Vector3(_moveDir.x, 0, 0).normalized;
                _canMove = _moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirX, _moveDistance);

                if (_canMove)
                {
                    // Can move only on the X
                    _moveDir = moveDirX;
                }
                else
                {
                    // Cannot move only on the X
                    // Attempt only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, _moveDir.z).normalized;
                    _canMove = _moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirZ, _moveDistance);

                    if (_canMove)
                    {
                        // Can move only on the Z
                        _moveDir = moveDirZ;
                    }
                    else
                    {
                        //Cannot move in any direction
                    }
                }
            }
            if (_canMove)
            {
                transform.position += _moveDir * _moveDistance;
            }

            if (_moveDir != Vector3.zero)
            {
                transform.forward = Vector3.Lerp(transform.forward, _moveDir, RotateSpeed * Time.deltaTime);
            }
        }
        
        private void GetMoveDirections()
        {
            _inputVector = gameInput.GetMovementVectorNormalized();
            _moveDir = new Vector3(_inputVector.x, 0, _inputVector.y);
            _moveDistance = moveSpeed * Time.deltaTime;
        }
    }
}
