using UnityEngine;

namespace _GameData.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 cameraOffsets;

        private Vector3 _playerPosition;
        private void LateUpdate()
        {
            _playerPosition = player.transform.position;
            transform.position = new Vector3(_playerPosition.x + cameraOffsets.x, cameraOffsets.y,
                _playerPosition.z + cameraOffsets.z);
            
            transform.LookAt(player);
        }
    }
}
