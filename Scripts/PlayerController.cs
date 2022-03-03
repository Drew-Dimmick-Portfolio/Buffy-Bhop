using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform obj;
    public float jumpSpeed;
    public float moveSpeed;
    public static float platformSpeed;
    private bool startAccelerating;
    private float accelerationStart;
    private float accelerationStartTimer;
    private bool cappedSpeed;
    private float platformSpeedCap;
    private float speedCapTimer;
    private float speedIncreaseLimit;
    private float speedIncreaseDuration;
    private float speedTimer;
    private bool inbounds;
    private string buttonPressed;

    public int score;
    private float scoreTimer;
    private float scoreTimerLimit;
    public Text scoreText;
    public Text finalScore;

    public GameObject menuUI;
    public GameObject displayedScore;
    public GameObject music;
    private bool paused = false;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inbounds = true;
        jumpSpeed = 7.5f;
        platformSpeed = 5;
        startAccelerating = false;
        accelerationStart = 25;
        accelerationStartTimer = 0;
        cappedSpeed = false;
        platformSpeedCap = 30;
        speedCapTimer = 0;
        speedIncreaseLimit = 5;
        speedIncreaseDuration = 5;
        speedTimer = 0;
        score = 0;
        scoreTimer = 0;
        scoreTimerLimit = 0.5f;
}

    void Update()
    {
        //obj.rotation = 0;

        float xMove = Input.GetAxis("Horizontal");
        if (inbounds)
        {
            rb.velocity = new Vector2(xMove * moveSpeed, rb.velocity.y);
        }

        if(!cappedSpeed && startAccelerating)
        {
            if (speedTimer > speedIncreaseLimit)
            {
                platformSpeed += Time.deltaTime;
            }

            if (speedTimer > (speedIncreaseDuration + speedIncreaseLimit))
            {
                speedTimer = 0;
            }

            speedTimer += Time.deltaTime;
            speedCapTimer += Time.deltaTime;
            
        }

        accelerationStartTimer += Time.deltaTime;
        scoreTimer += Time.deltaTime;

        if (accelerationStartTimer > accelerationStart)
        {
            startAccelerating = true;
        }

        if(speedCapTimer > platformSpeedCap)
        {
            cappedSpeed = true;
        }

        if(scoreTimer > scoreTimerLimit)
        {
            float tempScore = score + platformSpeed;
            score = (int)tempScore;
            scoreTimer = 0;
        }

        scoreText.text = "Score: " + score.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "StandardPlatform")
        {
            Jump();
        }
        else if(col.tag == "Limit")
        {
            inbounds = false;
        }
        else if(col.tag == "death")
        {
            Pause();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Limit")
        {
            inbounds = true;
        }
    }

    void Jump()
    {
        //Debug.Log("Jumping");
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
    }

    void Resume()
    {
        menuUI.SetActive(false);
        displayedScore.SetActive(true);
        music.SetActive(true);
        Time.timeScale = 1f;
        paused = true;
    }

    void Pause()
    {
        menuUI.SetActive(true);
        displayedScore.SetActive(false);
        music.SetActive(false);
        finalScore.text = "Score: " + score.ToString();
        Time.timeScale = 0f;
        paused = true;
    }

    public void PlayAgain()
    {
        Debug.Log("Playing Again...");
        SceneManager.LoadScene("Game");
        Resume();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
