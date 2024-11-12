using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void CheckForGameOver()
    {
        if (CrowdManager.Instance.crowdMembers.Count == 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Все персонажи погибли. Игра окончена.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
