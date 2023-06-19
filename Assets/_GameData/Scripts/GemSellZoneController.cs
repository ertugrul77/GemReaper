using System.Collections.Generic;
using _GameData.Scripts.Managers;
using _GameData.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GameData.Scripts
{
    public class GemSellZoneController : MonoBehaviour
    {        
        [Tooltip("If you want to edit the size of the SellZone, simply adjust the size of the visual.")]
        [Header("If you want to edit the size of the SellZone, simply adjust the size of the visual.")]
        [SerializeField] private Transform visual;

        public Transform lastGemTransform;
        private Vector3 _cubeVisualScale;
        private int _width;
        private int _lenght;
        private int _capacity;
        private int _xPos;
        private int _zPos;
        private int _yPos = 0;

        private readonly List<GemController> _soldGemControllerList = new List<GemController>(); 

        private void Awake()
        {
            AddCollider();
        }

        public void AddTheSoldGemToList(GemController gemController)
        {
            EventManager.RaiseOnGemSold(gemController);
            UIManager.Instance.EarnCoin(gemController);
        }

        public Vector3 SetLastStackPosition(GemController gemController)
        {
            if (_capacity >= _width * _lenght)
            {
                _soldGemControllerList.Clear();
                _yPos++;
            }
            _soldGemControllerList.Add(gemController);
            _capacity = _soldGemControllerList.Count;

            _xPos = (_capacity - 1) % _width;
            _zPos = -((_capacity - 1) / _width);

            return new Vector3(_xPos, _yPos, _zPos);
        }

        private const float BoxColliderYSize = 0.6f;

        private void AddCollider()
        {
            var localScale = visual.transform.localScale;
            var xScale =  localScale.x;
            var zScale =  localScale.z / 2f;
            
            _cubeVisualScale = new Vector3(xScale, BoxColliderYSize, zScale);
            transform.AddComponent<BoxCollider>().size = _cubeVisualScale;
            transform.GetComponent<BoxCollider>().center = new Vector3(0, 0, zScale);
            transform.GetComponent<BoxCollider>().isTrigger = true;
            
            SetLastGemPos();
            SetSellZoneCapacity();
        }

        private void SetSellZoneCapacity()
        {
            _width = (int)_cubeVisualScale.x;
            _lenght = (int)_cubeVisualScale.z;
        }

        private const float YScaleRatio = .83f;
        private const float LastGemPosXOffset = .4f;

        private void SetLastGemPos()
        {
            lastGemTransform.transform.localPosition = new Vector3(-_cubeVisualScale.x / 2,
                visual.transform.localScale.y * YScaleRatio, _cubeVisualScale.z / 2) + new Vector3(LastGemPosXOffset, 0, 0);
        }
    }
}
