using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [HideInInspector] public static bool canPauseGame = false;

    public GameObject PauseMenuPanel;
    public GameObject PauseFirstButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(canPauseGame)
            {
                if (GameIsPaused)
                    Resume();

                else
                    Pause();
            }
        }
    }

    private void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
    }
    private void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void DelayedResume()
    {
        StartCoroutine(ResumeCoroutine());
    }
    private IEnumerator ResumeCoroutine()
    {
        yield return null;
        Resume();
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
