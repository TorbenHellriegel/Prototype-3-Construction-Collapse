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
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || hasNotDoubleJumped) && !gameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if(isOnGround)
            {
                isOnGround = false;
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
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            hasNotDoubleJumped = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
        }
    }
}
