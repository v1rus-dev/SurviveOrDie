using UnityEngine;

namespace Server
{
    public class SpawnPlayers : MonoBehaviour
    {
    
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject[] spawnPoints;
        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
        }
        
        //Spawn players
        public void SpawnPlayer()
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(player, spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
        }
    }
}
