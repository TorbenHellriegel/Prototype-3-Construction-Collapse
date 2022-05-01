using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10;
    private float leftBoundry = -10;
    private bool obstaclePassedPlayer = false;

    public PlayerController playerControllerScript;
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves left as long as the game isnt over
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left  * Time.deltaTime * speed * playerControllerScript.movementSpeedMutiplyer);
        }

        // Increase the score when an obstacle passes the player
        if(transform.position.x < -1 && gameObject.CompareTag("Obstacle") && obstaclePassedPlayer == false)
        {
            scoreManager.AddScore(100.0f);
            obstaclePassedPlayer = true;
        }
        
        // Destroy the game object when it falls below the grond
        if(transform.position.y < leftBoundry && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
