using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    public Button playButton; 

    void Start()
    {       
        playButton.onClick.AddListener(PlayGame);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }
}
