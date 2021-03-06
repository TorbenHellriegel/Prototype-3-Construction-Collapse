using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] debrisPrefabs;
    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    private Vector3 dropPosition = new Vector3(20, 13, 3);
    private float startDelay = 3;
    private float repeatRateMin = 1;
    private float repeatRateMax = 4;
    private float debrisRepeatRate = 0.6f;

    public PlayerController PlayerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Start spawning in obstacles
        Invoke("SpawnObstacle", startDelay);
        Invoke("SpawnDebris", startDelay);
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

    // Repetedly spawns falling debris
    private void SpawnDebris()
    {
        int debrisIndex = Random.Range(0, debrisPrefabs.Length);

        Instantiate(debrisPrefabs[debrisIndex], dropPosition, debrisPrefabs[debrisIndex].transform.rotation);
        
        // Spawns another object if the game isnt over
        if(PlayerControllerScript.gameOver == false)
        {
            Invoke("SpawnDebris", debrisRepeatRate);
        }
        
    }
}
