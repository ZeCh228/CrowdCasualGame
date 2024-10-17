using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject winUIPanel; // ������ UI, ������� ������������ ��� ������
    public GameObject ChelCaunterUI; // ������ UI, ������� ������������ ��� ������
    public FinishZone finishZone; // ������ �� �������� ���� ��� ������������
    private bool gameWon = false;

    void Update()
    {
        // ���������, �������� �� ���� ����������
        if (finishZone.AreAllCharactersSucked() && !gameWon)
        {
            TriggerWin();
        }
    }

    // �����, ���������� ��� ������
    void TriggerWin()
    {
        gameWon = true;

        // ������������� �����
        Time.timeScale = 0f;

        // ���������� UI ������
        winUIPanel.SetActive(true);

        ChelCaunterUI.SetActive(false);
    }
}
