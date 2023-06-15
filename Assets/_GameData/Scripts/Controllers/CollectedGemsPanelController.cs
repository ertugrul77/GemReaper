using System.Collections.Generic;
using _GameData.Scripts.Managers;
using _GameData.Scripts.ScriptableObjects;
using UnityEngine;

namespace _GameData.Scripts.Controllers
{
    public class CollectedGemsPanelController : MonoBehaviour
    {
        [SerializeField] private Transform panel;
        [SerializeField] private GameObject gemView;
        [SerializeField] private AllGemList allGemList;

        private readonly List<GemViewController> _gemViewControllerList = new List<GemViewController>();

        private void Awake()
        {
            EventManager.OnGemSold += OnGemSold;
            CreateGemViews();
        }

        private void OnGemSold(GemController gemController)
        {
            for (int i = 0; i < _gemViewControllerList.Count; i++)
            {
                if (gemController.gemIdentity.type == _gemViewControllerList[i].gemType)
                {
                    IncreaseGemCount(i);
                }
            }
        }
        private void CreateGemViews()
        {
            foreach (var gemIdentity in allGemList.gemIdentityList)
            {
                var gemViewGameObject = Instantiate(gemView, panel.transform);
                GemViewController gemViewController = gemViewGameObject.GetComponent<GemViewController>();
                
                SetGemViewData(gemViewController, gemIdentity);
                _gemViewControllerList.Add(gemViewController);
            }
        }
        private void SetGemViewData(GemViewController gemViewController, GemIdentity gemIdentity)
        {
            gemViewController.gemType = gemIdentity.type;
            gemViewController.gemImage.sprite = gemIdentity.iconSprite;
            gemViewController.gemName.text = gemIdentity.type.ToString();
            gemViewController.gemName.color = SetTextColor(gemIdentity.type);
            
            gemViewController.gemCount = PlayerPrefs.GetInt(gemIdentity.type.ToString());
            gemViewController.gemCountText.text = "Collected: " + gemViewController.gemCount;

        }
        private void IncreaseGemCount(int index)
        {
            _gemViewControllerList[index].gemCount++;
            _gemViewControllerList[index].gemCountText.text = "Collected: " + _gemViewControllerList[index].gemCount;
            var gemTypeString = _gemViewControllerList[index].gemType.ToString();
            PlayerPrefs.SetInt(gemTypeString, _gemViewControllerList[index].gemCount);
        }
        private Color SetTextColor(GemType gemType)
        {
            Color color;
            switch (gemType)
            {
                case GemType.GemBlue :
                    color = Color.blue;
                    color.a = .7f;
                    return color;
                case GemType.GemGreen :
                    color = Color.green;
                    color.a = .7f;
                    return color;
                case GemType.GemPink:
                    color = Color.magenta;
                    color.a = .7f;
                    return color;
                case GemType.GemRed:
                    color = Color.red;
                    color.a = .7f;
                    return color;
                case GemType.GemYellow:
                    color = Color.yellow;
                    color.a = .7f;
                    return color;
            }
            return default;
        }
    
    }
}
