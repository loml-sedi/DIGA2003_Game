using UnityEngine;
using UnityEngine.SceneManagement;

/*Rehope Games (2023) How to Create a PAUSE MENU in Unity! | UI Design Tutorial. 6 June. [Online] Availabe at : https://www.youtube.com/watch?v=MNUYe0PWNNs (Accessed : 16 May 2025)  */
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
