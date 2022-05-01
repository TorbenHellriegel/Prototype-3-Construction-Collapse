using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLeft : MonoBehaviour
{
    private float speed = 10;
    private float leftBoundry = -10;
    public PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves left as long as the game isnt over
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left  * Time.deltaTime * speed * playerControllerScript.movementSpeedMutiplyer);
        }
        
        // Destroy the game object when it falls below the grond
        if(transform.position.y < leftBoundry)
        {
            Destroy(gameObject);
        }
    }
}
