using UnityEngine;
using System.Collections;

/*
 * AIA, 2021. Unity UI Tutorial. [Online] Available at: https: https://youtu.be/IuuKUaZQiSU?si=Vmx0VfW3Wnrip7oN (Accessed: 18 April 2025) 
 * GameDevGuide, 2021. Unity UI canvas modes and canvas scaler explained. [Online] Available at: https://www.youtube.com/watch?v=1OwQflHq5kg (Accessed: 19 April 2025) 
 * Brackeys, 2017. How to Make a Fade to Black Screen in Unity. [Online] Available at: https://www.youtube.com/watch?v=0HwZQt94uHQ (Accessed: 18 April 2025) 
 */

public class endGame : MonoBehaviour
{
    [SerializeField] private GameObject blackScreenPanel;
    [SerializeField] private PuppetMove puppetScript;
    private bool _hasTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasTriggered && puppetScript.IsFollowing)
        {
            _hasTriggered = true;
            StartCoroutine(ShowBlackScreen());
            Ending(); // Ending the Scene.
            
        }
    }
    
    private IEnumerator ShowBlackScreen()
    {
        blackScreenPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
    }
    void Ending()
    {
        if (QUIT.instance != null)
        {
            QUIT.instance.Quitgame();
            print("End of Scene");
           
        }
       
    }
}
