using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private float leftBoundry = -10;

    public PlayerController PlayerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves left as long as the game isnt over
        if(PlayerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left  * Time.deltaTime * speed);
        }
        
        // Destroy the game object when it falls below the grond
        if(transform.position.y < leftBoundry && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
