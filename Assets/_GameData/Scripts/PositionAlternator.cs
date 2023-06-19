using System.Collections;
using System.Collections.Generic;
using _GameData.Scripts.ScriptableObjects;
using UnityEngine;

namespace _GameData.Scripts
{
    public class PositionAlternator : MonoBehaviour
    {

        [SerializeField] private List<MoveCommand> moveCommands;
        [SerializeField] private bool randomizeTimeInterval;
        [SerializeField] private float lerpSpeed = 1.4f;
        private Vector3 _currentTarget;
        private Vector3 _originalPos;
        private Vector3 _offsettedPosition;
        private bool _isReturningOriginalPos;
        private bool _isCollected;

        private void Start()
        {
            _originalPos = transform.localPosition;
            _offsettedPosition = _originalPos;
            foreach (var moveCommand in moveCommands)
            {
                var moveDirection = moveCommand.MoveAxis switch
                {
                    MoveAxis.X => Vector3.right,
                    MoveAxis.Y => Vector3.up,
                    _ => Vector3.forward
                };
                _offsettedPosition += moveDirection * moveCommand.MoveOffset;
            }
            _currentTarget = _offsettedPosition;
            _isReturningOriginalPos = false;
            if (randomizeTimeInterval) lerpSpeed -= Random.Range(0, .2f);

        }

        private void Update()
        {
            if (_isCollected) return;
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, _currentTarget, Time.deltaTime * lerpSpeed);
            
            if (!((transform.localPosition - _currentTarget).sqrMagnitude < .01)) return;
            _currentTarget = _isReturningOriginalPos ? _offsettedPosition : _originalPos;
            _isReturningOriginalPos = !_isReturningOriginalPos;
        }

        public void SetMovingPosition(bool isCollected)
        {
            _isCollected = isCollected;
        }

        public enum MoveAxis
        {
            X,
            Y,
            Z
        }
    
    }
}


