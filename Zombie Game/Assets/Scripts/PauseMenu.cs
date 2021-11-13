using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Menu;
    Player player;


    private void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }

        player = FindObjectOfType<Player>();
        if (!player) PauseGame();
    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Menu.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
        ResumeGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
