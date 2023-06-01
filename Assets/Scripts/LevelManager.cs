using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Main variable that controls the game state behavior
    public static bool isGameOver;

    // Sound effects for when the player wins or loses
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    
    // Camera AudioSource to play the audio to
    public AudioSource cameraAudioSource;

    // Players running score count
    public int score;
    public Text scoreText;

    // Text to appear when a level is beat or lost
    public Text gameText;


    void Start()
    {
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        initializeSceneSettings();
    }

    void Update()
    {
        
    }

    // Method to perform when the level is lost
    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "You Died";
        gameText.color = Color.red;
        gameText.gameObject.SetActive(true);

        cameraAudioSource.PlayOneShot(gameOverSFX);

        Invoke("LoadCurrentLevel", 2);
    }

    // Method to perform when the level is beat
    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "You Win";
        gameText.color = Color.green;
        gameText.gameObject.SetActive(true);

        cameraAudioSource.PlayOneShot(gameWonSFX);

        Invoke("LoadNextLevel", 2);
    }

    // Loads the next scene using the SceneManager
    void LoadNextLevel()
    {
        initializeSceneSettings();
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
    }

    // Loads the current scene from the start
    void LoadCurrentLevel()
    {
        initializeSceneSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Initializes the scene's settings
    // Level names in the switch need to match exactly!!!
    private void initializeSceneSettings()
    {
        isGameOver = false;
        score = 0;
        scoreText.text = score.ToString();
        gameText.gameObject.SetActive(false);

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                // Add some level specific variable settings here
                // Ex.
                // levelDuration = 15f;
                // pickupCount = 4;
                break;
            case "Level2":
                break;
            case "Level3":
                break;
        }
    }
}
