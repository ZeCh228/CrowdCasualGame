using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Этот метод будет вызван, когда все персонажи погибнут
    public void CheckForGameOver()
    {
        if (CrowdManager.Instance.crowdMembers.Count == 0)
        {
            GameOver();
        }
    }

    // Метод, вызываемый при проигрыше
    void GameOver()
    {
        Debug.Log("Все персонажи погибли. Игра окончена.");
        // Перезагрузка сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
