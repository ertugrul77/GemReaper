using System;
using _GameData.Scripts.Managers;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

namespace _GameData.Scripts.UI
{
    public class CoinUIManager : MonoBehaviour
    {
        [SerializeField] private Image coinImage;
        [SerializeField] private Transform targetPos;

        private Camera _cam;
        
        private void Start()
        {
            _cam = Camera.main;
            EventManager.OnGemSold += OnGemSold;
        }

        private void OnGemSold(GemController gemController)
        {
            var gemWorldPosition = _cam.WorldToScreenPoint(gemController.transform.position);
            var coinTransform = Instantiate(coinImage, gemWorldPosition, Quaternion.identity, transform);
            
            coinTransform.transform.DOMove(targetPos.position, .2f).OnComplete(() =>
            {
                Destroy(coinTransform.gameObject);
            });
        }
    }
}
