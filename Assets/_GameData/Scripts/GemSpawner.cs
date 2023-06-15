using _GameData.Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameData.Scripts
{
    public class GemSpawner : MonoBehaviour
    {
        [SerializeField] private AllGemList allGemList;
        [SerializeField] private GameObject gemSpawnerGround;
        [SerializeField] private float cellSize;
        [SerializeField] private int width;
        [SerializeField] private int height;

        private Vector3 _startPos;
        private int[,] _gridArray;
        private int _tempWidth;
        private int _tempHeight;
        
        private void Start()
        {
            CreateSpawnerGround();
            SpawnGem();            
        }

        public void SpawnNewGem(Vector3 position)
        {
            GameObject gem = GenerateRandomGem();
            gem.transform.position = position;
            gem.GetComponent<GemController>().gemSpawner = this;
        }
        private void SpawnGem()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector3 gemPosition = new Vector3(i, 0.1f, j) * cellSize + transform.position;
                    SpawnNewGem(gemPosition);
                }
            }
        }
        private GameObject GenerateRandomGem()
        {
            int randomIndex = Random.Range(0, allGemList.gemIdentityList.Count);
            GameObject gem = Instantiate(allGemList.gemIdentityList[randomIndex].prefab, transform);
            return gem;
        }
        
        private void CreateSpawnerGround()
        {
            gemSpawnerGround.transform.localScale = new Vector3(width * cellSize, 0.1f, height* cellSize);
            gemSpawnerGround.transform.localPosition = new Vector3((width-1) * cellSize * 0.5f, 0, (height -1) * cellSize * 0.5f);
            gemSpawnerGround.SetActive(true);
        }
        private void OnDrawGizmos()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Gizmos.DrawWireCube(new Vector3(i * cellSize,0,j * cellSize) + transform.position,  new Vector3(cellSize,0,cellSize));
                }
            }
        }
    }

}
