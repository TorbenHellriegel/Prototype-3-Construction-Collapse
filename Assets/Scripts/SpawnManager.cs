using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startDelay = 3;
    private float repeatRateMin = 1;
    private float repeatRateMax = 4;

    public PlayerController PlayerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnObstacle", startDelay);
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Repetedly spawns in obstacles
    private void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        float repeatRate = Random.Range(repeatRateMin, repeatRateMax);

        Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
        
        // Spawns another object if the game isnt over
        if(PlayerControllerScript.gameOver == false)
        {
            Invoke("SpawnObstacle", repeatRate);
        }
        
    }
}
