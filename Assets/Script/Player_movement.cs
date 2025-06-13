using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_movement : MonoBehaviour
{
    public float moveSpeed = 10f;          // Speed for horizontal movement
    public float jumpForce = 15f;          // Jump force (optional)
    public float gravity = 9.81f;          // Gravity
    public float forwardSpeed = 4f;        // Constant forward movement
    public float minX = -0.60f;            // Left boundary
    public float maxX = 0.60f;             // Right boundary

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private bool doublejump = false;      // Double jump
    private bool player_is_live = true;
    


    private Animator ani;

    public AudioSource Audio;
    public TMP_Text scoreText;        // Reference to the UI Text for current score
    public TMP_Text highScoreText;    // Reference to the UI Text for high score
    private int currentScore = 0; // Current score in the game
    private int highScore = 0;    // High score to be displayed
    public TMP_Text coin_scoreText;        // Reference to the UI Text for coin current score
    public TMP_Text coin_scoreText1;
    public TMP_Text scoreText1;
    public TMP_Text coin_highScoreText;    // Reference to the UI Text for coin high score
    private int coin_currentScore = 0; //coin Current score in the game
    
    public AudioClip buttonSound;     // The sound clip you want to play
    public AudioClip flower_sound;
    
    public GameObject Retry_panel; // retry function
    public GameObject Pasue_panel; // pasue function

   


     
    // Start is called before the first frame update
    void Start()
    {
        // Get the CharacterController component attached to the player
        controller = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        Retry_panel.SetActive(false);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = " " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_is_live)
        {
            MovePlayer(); // Call movement only if player is live
            IncreaseScore(10);

        }

        // Method to handle horizontal and forward movement
        void MovePlayer()
        {

            // Horizontal input (A/D or arrow keys)
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calculate movement direction
            moveDirection.x = horizontalInput * moveSpeed;


            moveDirection.z = forwardSpeed;


            if (controller.isGrounded)
            {
                // Allow jump when grounded
                if (Input.GetButtonDown("Jump"))
                {
                    doublejump = true;
                    moveDirection.y = jumpForce;
                    ani.SetTrigger("Runing_Flip");
                }
                else
                {
                    moveDirection.y = -1f; // keeps player grounded
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") && doublejump)
                {
                    moveDirection.y = jumpForce * 2;
                    ani.SetTrigger("Runing_Flip");
                    doublejump = false;
                }
                // Apply gravity while in air
                moveDirection.y -= gravity * Time.deltaTime;
            }
            // Move the character
            controller.Move(moveDirection * Time.deltaTime);

            // Clamp the player's position manually
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            transform.position = clampedPosition;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Detect ground collision
        if (other.gameObject.CompareTag("Flower"))
        {
            Destroy(other.gameObject);
            Audio.PlayOneShot(flower_sound);
            coin_currentScore++;
            coin_scoreText.text = " " + coin_currentScore;
            coin_scoreText1.text = " " + coin_currentScore;
            coin_highScoreText.text = "" + coin_currentScore;

        }
        if (other.gameObject.CompareTag("Spider") || other.gameObject.CompareTag("Tree"))
        {
            
            ani.SetTrigger("Fall_Flat");
            forwardSpeed = 0;
            StartCoroutine(ShowRetryPanelAfterDelay(3f)); // Wait for 2 seconds
            Pasue_panel.SetActive(false);

        }
        IEnumerator ShowRetryPanelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Retry_panel.SetActive(true);
            Pasue_panel.SetActive(false);
        }
    }


    // highest score save 
    public void IncreaseScore(int amount)
    {
        if (!player_is_live) return;
        currentScore += amount;
        scoreText.text = " " + currentScore.ToString();
        scoreText1.text = " " + currentScore.ToString();

        // Check if current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = " " + highScore.ToString();

            // Save the new high score using PlayerPrefs
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }


    // Retry_Button function
    public void Retry_Button()
    {
        Audio.PlayOneShot(buttonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("SampleScene");
        Debug.Log("Reset button wrk");
    }
    
    public void Pasum_Button()
    {
        player_is_live = false;
        Audio.PlayOneShot(buttonSound);
        Pasue_panel.SetActive(true);
        Retry_panel.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume_Button()
    {
        player_is_live = true;
        Audio.PlayOneShot(buttonSound);
        Pasue_panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Quit_Button()
    {
        Audio.PlayOneShot(buttonSound);
        Application.Quit();
    }
}
