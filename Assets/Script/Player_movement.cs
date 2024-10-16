using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Player_movement : MonoBehaviour
{
    public float moveSpeed = 15f;            // Speed for horizontal movement
    public float jumpForce = 15f;            // Jump force for vertical movement
    public float gravity = 9.81f;              // Gravity affecting the player
    public float forwardSpeed = 5f;          // Constant forward speed
    private float minX = -2.7f;              // Left boundary for movement
    private float maxX = 2.7f;               // Right boundary for movement

    private Vector3 moveDirection;           // Current movement direction
    private CharacterController controller;  // Reference to the CharacterController component

    private bool isGrounded;                 // Check if the player is grounded
    private Animator ani;
    
    public GameObject Retry_panel;
    public AudioSource Audio;
    public AudioClip Coin_Sound;
    public TMP_Text scoreText;        // Reference to the UI Text for current score
    public TMP_Text highScoreText;    // Reference to the UI Text for high score
    private int currentScore = 0; // Current score in the game
    private int highScore = 0;    // High score to be displayed
    public TMP_Text coin_scoreText;        // Reference to the UI Text for coin current score
    public TMP_Text coin_highScoreText;    // Reference to the UI Text for coin high score
    private int coin_currentScore = 0; //coin Current score in the game
    //private int coin_highScore = 0;    //  coin High score to be displayed

    // Start is called before the first frame update
    void Start()
    {
        // Get the CharacterController component attached to the player
        controller = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        Retry_panel.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();   // Handle horizontal and forward movement
        HandleJump();       // Handle jumping
        player_slide();
        IncreaseScore(10);
      
    }

    // Method to handle horizontal and forward movement
    private void HandleMovement()
    {
        // Get horizontal input (A/D keys or Left/Right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the desired movement direction
        moveDirection = new Vector3(horizontalInput * moveSpeed, moveDirection.y,forwardSpeed);

        // Clamp the player's X position to keep them within bounds
        moveDirection.x = Mathf.Clamp(moveDirection.x, minX, maxX);

        // Apply gravity to the player's Y movement
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character based on the calculated direction
        controller.Move(moveDirection * Time.deltaTime);
    }

    // Method to handle jumping
    private void HandleJump()
    {
        // Check if the player is grounded (CharacterController's `isGrounded` property)
        if (controller.isGrounded)
        {
            isGrounded = true;

            // If the player presses the jump button (spacebar), apply jump force
            if (Input.GetButtonDown("Jump"))
            { 
                ani.SetTrigger("Runing_Flip");
                moveDirection.y = jumpForce;
                isGrounded = false;
            }
        }
    }

    private void player_slide()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            {
                ani.SetTrigger("Slide"); 
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Detect ground collision
        if (other.gameObject.CompareTag("Coin"))
        {
                Destroy(other.gameObject);
                coin_currentScore++;
                coin_scoreText.text = " " + coin_currentScore;
                coin_highScoreText.text = "" + coin_currentScore;
                Audio.PlayOneShot(Coin_Sound);
        }
    }

     private void OnCollisionEnter(Collision collision)
    {
        // Detect ground collision
        if (collision.gameObject.CompareTag("Trap"))
        {
            ani.SetTrigger("Daying");
            Destroy(this.gameObject,3f);
            Retry_panel.SetActive(true);
            
        }
    }

     // highest score save 
    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score: " + currentScore.ToString();

        // Check if current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "High Score: " + highScore.ToString();

            // Save the new high score using PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
