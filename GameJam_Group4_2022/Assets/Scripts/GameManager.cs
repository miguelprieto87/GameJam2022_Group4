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
    public int levelWidth;
    public int levelLength;

    public GameObject winScreen;
    public GameObject failScreen;

    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Update()
    {
        if (playerState == playerState.End) if (winScreen.activeSelf != true) winScreen.SetActive(true);
        if (playerState == playerState.Failed) if (failScreen.activeSelf != true) failScreen.SetActive(true);
    }

    public void changeScenes(string sceneName)
    {
        if (sceneName == "")
        {
            Debug.Log("No scene listed, reloading current scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else SceneManager.LoadScene(sceneName);
    }
}
