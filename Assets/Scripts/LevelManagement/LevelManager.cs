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

    // Text to appear when a level is beat or lost
    public Text gameText;
    public string nextLevel;


    void Start()
    {
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        InitializeSceneSettings();
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
        gameText.color = Color.green;
        gameText.gameObject.SetActive(true);

        // Sets the game text to the correct value, and loads the next level if there is one
        if (!string.IsNullOrEmpty(nextLevel))
        {
            gameText.text = "LEVEL COMPLETE!";
            Invoke("LoadNextLevel", 3.5f);
        }
        else
        {
            gameText.text = "YOU WIN!";
            Invoke("LoadMainMenu", 5f);
        }
    }

    // Loads the next scene using the SceneManager
    void LoadNextLevel()
    {
        InitializeSceneSettings();
        SceneManager.LoadScene(nextLevel);
    }

    // Loads the current scene from the start
    void LoadCurrentLevel()
    {
        InitializeSceneSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Initializes the scene's settings
    private void InitializeSceneSettings()
    {
        isGameOver = false;
        gameText.gameObject.SetActive(false);
    }

    private void LoadMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
