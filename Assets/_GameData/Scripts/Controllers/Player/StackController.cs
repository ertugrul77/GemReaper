using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GameData.Scripts.Managers;
using UnityEngine;

namespace _GameData.Scripts.Controllers.Player
{
    public class StackController : MonoBehaviour
    {
        [SerializeField] private Transform lastStackPosition;
        [SerializeField] private List<GemController> collectedGemList;

        private bool _isSelling;
        private GameObject _tempLastGem;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GemController gemController))
            {
                // Player got a Gem
                gemController.SetCollider(false);
                gemController.StopTween();
                StackGem(gemController);
                gemController.gemSpawner.SpawnNewGem(gemController.transform.position);
                gemController.positionAlternator.SetMovingPosition(true);
                gemController.gemShadow.SetActive(false);
                AudioReactor.Play(AudioReactor.lib.gemPing);
            }
            else if (other.TryGetComponent(out GemSellZoneController gemSellZoneController))
            {
                // Player entered the GemSellZone
                StartCoroutine(SellingGems(gemSellZoneController));
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GemSellZoneController gemTakerController))
            {
                // Player exited the GemSellZone
                _isSelling = false;
                StopCoroutine(SellingGems(gemTakerController));
            }
        }

        private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(.1f);
        private IEnumerator SellingGems(GemSellZoneController gemSellZoneController)
        {
            _isSelling = true;
            while (_isSelling && collectedGemList.Count > 0)
            {
                AudioReactor.Play(AudioReactor.lib.getMoney);
                var lastGem = collectedGemList.Last();
                SetLastStackPosition(-lastGem.GetYBoundsSize());
                lastGem.SetParent(gemSellZoneController.lastGemTransform);
                lastGem.GemMovement(gemSellZoneController.SetLastStackPosition(lastGem));
                gemSellZoneController.AddTheSoldGemToList(lastGem);
                collectedGemList.Remove(collectedGemList.Last());
                
                yield return _waitForSeconds;
            }
        }
        private void StackGem(GemController gemController)
        {
            gemController.SetParent(transform);
            gemController.GemMovement(lastStackPosition.localPosition);
            SetLastStackPosition(gemController.GetYBoundsSize());
            collectedGemList.Add(gemController);
        }
        private void SetLastStackPosition(float gemYScale)
        {
            lastStackPosition.transform.localPosition += new Vector3(0, gemYScale, 0);
        }
    }
}
