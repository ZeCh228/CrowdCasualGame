using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject winUIPanel; // Панель UI, которая отображается при победе
    public GameObject ChelCaunterUI; // Панель UI, которая отображается при победе
    public FinishZone finishZone; // Ссылка на финишную зону для отслеживания
    private bool gameWon = false;

    void Update()
    {
        // Проверяем, засосало ли всех персонажей
        if (finishZone.AreAllCharactersSucked() && !gameWon)
        {
            TriggerWin();
        }
    }

    // Метод, вызываемый при победе
    void TriggerWin()
    {
        gameWon = true;

        // Останавливаем время
        Time.timeScale = 0f;

        // Показываем UI победы
        winUIPanel.SetActive(true);

        ChelCaunterUI.SetActive(false);
    }
}
