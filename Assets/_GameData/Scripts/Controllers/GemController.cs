using _GameData.Scripts.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace _GameData.Scripts
{
    public class GemController : MonoBehaviour
    {
        [HideInInspector] public GemSpawner gemSpawner;
        public GemIdentity gemIdentity;
        public PositionAlternator positionAlternator;
        public GameObject gemShadow;

        private Collider _collider;
        private Tween _grownTween;
        private float _scaleValue;
        private float _gemScale;
        private bool _grown;
        private float _setter;
        private const float EndScaleValue = 1f;
        private const float Duration = 5f;
        private const float MinGrownValue = 0.25f;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            SetCollider(false);
        }
        private void Start()
        {
            GrowUp();
        }
        private void GrowUp()
        {
            _grownTween = DOTween.To(()=> _setter, x=> _setter = x, EndScaleValue, Duration).OnUpdate(() =>
            {
                transform.localScale = Vector3.one * _setter;
                if (_setter >= MinGrownValue)
                {
                    SetCollider(true);
                }
            }).SetEase(Ease.InOutBounce).OnComplete(() =>
            {
                var particle = Instantiate(gemIdentity.grownParticle, transform);
                particle.transform.localPosition = new Vector3(0, transform.localScale.y,0);
                particle.Play();
            });
        }
        public float GetYBoundsSize()
        {
            return GetComponentInChildren<Renderer>().bounds.size.y;
        }
        public void GemMovement(Vector3 targetPosition)
        {
            transform.DOLocalMove(targetPosition, .1f).SetEase(Ease.OutBounce);
        }
        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
        public void StopTween()
        {
            _grownTween.Kill();
        }
        public void SetCollider(bool colliderActivity)
        {
            _collider.enabled = colliderActivity;
        }
    }
}
