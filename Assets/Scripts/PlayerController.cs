using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    public float gravityModefyer;
    public bool isOnGround = true;
    public bool hasNotDoubleJumped = true;
    public bool gameOver;

    private Rigidbody playerRigidbody;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModefyer;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses space and is currently on the ground or
        // has not jet double jumped Then make the player jump
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || hasNotDoubleJumped) && !gameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Check if the player used a normal or double jump and disable ist use
            if(isOnGround)
            {
                isOnGround = false;
                // Trigger jump animation
                playerAnim.SetTrigger("Jump_trig");
            }
            else
            {
                hasNotDoubleJumped = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player hits the ground again restore both jumps
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            hasNotDoubleJumped = true;
        }
        // If the player hits an obstacle set game over true and trigger death animation
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
        }
    }
}
