using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public ScoreManager scoreManager;

    private float playerInput;
    public float jumpForce = 10;
    public float movementSpeedMutiplyer = 2;
    public float gravityModefyer;
    public bool isOnGround;
    public bool hasNotDoubleJumped = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModefyer;
        isOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses space and is currently on the ground or
        // has not jet double jumped Then make the player jump
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || hasNotDoubleJumped) && !gameOver)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.5f);

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
        // If the player presses space during game over restart the game
        else if(Input.GetKeyDown(KeyCode.Space) && gameOver)
        {
            SceneManager.LoadScene("Prototype 3");
            // Reset gravity to previous value to stop exponential gravity increase
            Physics.gravity /= gravityModefyer;
        }

        // Only receve the player input when the player is on the ground
        // This way the player cant change the speed mid jump
        if(isOnGround)
        {
            playerInput = Input.GetAxis("Horizontal");
        }

        // Change the speed of the game depending on the players input
        movementSpeedMutiplyer = 2 + playerInput;

        // Change the running animation to walking when the player slows down
        if(movementSpeedMutiplyer < 1.5f)
        {
            playerAnim.SetFloat("Speed_f", 0.4f);
            dirtParticle.Stop();
            // Decrease the score when the player runs too slow
            scoreManager.AddScore(-Time.deltaTime*10);
        }
        else if(movementSpeedMutiplyer > 1.5f)
        {
            playerAnim.SetFloat("Speed_f", 1.0f);
        }

        // Increase the speed of the running animation when the playe speeds up
        if(movementSpeedMutiplyer > 2.5f)
        {
            playerAnim.speed = 2;
            // Decrease the score when the player runs too slow
            scoreManager.AddScore(Time.deltaTime*10);
        }
        else if(movementSpeedMutiplyer < 2.5f)
        {
            playerAnim.speed = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player hits the ground again restore both jumps
        if(collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("Grounded", true);
            isOnGround = true;
            hasNotDoubleJumped = true;
            if(gameOver == false)
            {
                dirtParticle.Play();
            }
        }
        // If the player hits an obstacle set game over true and trigger death animation
        else if(collision.gameObject.CompareTag("Obstacle") && gameOver == false)
        {
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            scoreManager.GameOverScore();
        }
    }
}
