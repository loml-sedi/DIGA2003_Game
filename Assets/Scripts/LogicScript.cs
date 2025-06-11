using UnityEngine;
using UnityEngine.SceneManagement;

/*Game Maker’s Toolkit (2022) The Unity Tutorial For Complete Beginners. 2 December. [Online] Available at: https://www.youtube.com/watch?v=XtQMytORBmM&t=2011s ( Accessed: 14 April 2025). */

public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public void restartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void gameOver() => gameOverScreen.SetActive(true);
}
