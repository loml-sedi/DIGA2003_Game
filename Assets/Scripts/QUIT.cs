using UnityEngine;

public class QUIT : MonoBehaviour
{
    public static QUIT instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake() => instance = this;
    public void Quitgame()
    {
        Application.Quit();
    }
}
