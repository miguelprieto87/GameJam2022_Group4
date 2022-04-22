using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum playerState
{
    End,
    Failed,
    Neutral
}
public class GameManager : MonoBehaviour
{
    public playerState playerState;
    public static GameManager instance;
    public int levelX;
    public int levelZ;
    public bool isPaused;

    public GameObject winScreen;
    public GameObject failScreen;
    public GameObject pauseButton;
    
    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Update()
    {
        if (playerState == playerState.End)
        {
            if (winScreen.activeSelf != true)
            {
                FindObjectOfType<AudioManager>().Play("Win");
                winScreen.SetActive(true);
            }
        }
        if (playerState == playerState.Failed)
        {
            if (failScreen.activeSelf != true)
            {
                FindObjectOfType<AudioManager>().Play("Lose");
                failScreen.SetActive(true);
            }
        }

        if (playerState == playerState.End || playerState == playerState.Failed)
        {
            if (pauseButton.activeSelf == true)
            {
                
                pauseButton.SetActive(false);
            }
        } 
    }

    public void changeScenes(string sceneName)
    {
        if (sceneName == "")
        {
            Debug.Log("No scene listed, reloading current scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else SceneManager.LoadScene(sceneName);
        if (Time.timeScale < 1) Time.timeScale = 1;
    }

    //Pause component used by a button to freeze and unfreeze time
    public void pauseGame(int speed)
    {
        Time.timeScale = speed;
        if (speed > 0)
        {
            Debug.Log("Resuming Game");
            isPaused = false;
            
        }

        if (speed == 0)
        {
            Debug.Log("Game Paused");
            isPaused = true;
            
        }
    }
}
