using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject winUIPanel; 
    public GameObject ChelCaunterUI;
    public FinishZone finishZone;
    private bool gameWon = false;

    private void Update()
    {
        if (finishZone.AreAllCharactersSucked() && !gameWon)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        gameWon = true;
 
        Time.timeScale = 0f;
     
        winUIPanel.SetActive(true);

        ChelCaunterUI.SetActive(false);
    }
}
