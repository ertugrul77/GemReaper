using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GameData.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private GameObject collectedGemsUI;
        [SerializeField] private GameObject allGemsButtonUI;
        [SerializeField] private GameObject floatingJoyStick;
        [SerializeField] private Text coinText;
        
        private float _coinValue;
        private void Awake()
        {
            Instance = this;
            
            SetActivities(true);
            LoadCoinCount();
        }

        public void EarnCoin(GemController gemController)
        {
            var gemSize = gemController.transform.localScale.x;
            float f = gemSize;
            f = Mathf.Round(f * 100.0f) * 0.01f;
            _coinValue +=gemController.gemIdentity.price * f;
            
            PlayerPrefs.SetFloat("CoinValue", _coinValue);
            coinText.text = _coinValue.ToString(CultureInfo.InvariantCulture);
        }

        public void SetCollectedGemsUI(bool activity)
        {
            if (activity)
            {
                SetActivities(false);
                collectedGemsUI.transform.localScale = Vector3.zero;
                collectedGemsUI.transform.DOLocalMove(Vector3.zero, .3f);
                collectedGemsUI.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InOutExpo);
            }
            else
            {
                collectedGemsUI.transform.localScale = Vector3.one;
                collectedGemsUI.transform.DOMove(allGemsButtonUI.transform.position, .5f);
                collectedGemsUI.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutExpo).OnComplete(() =>
                {
                    SetActivities(true);
                });
            }
        }
        private void SetActivities(bool activity)
        {
            collectedGemsUI.SetActive(!activity);
            allGemsButtonUI.SetActive(activity);
            floatingJoyStick.SetActive(activity);
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void LoadCoinCount()
        {
            _coinValue = PlayerPrefs.GetFloat("CoinValue");
            coinText.text = _coinValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
