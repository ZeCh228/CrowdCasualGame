using TMPro;
using UnityEngine;

public class PassageZone : MonoBehaviour
{
    public TextMeshProUGUI counterText; // ��������� ������� ��� ����������� ��������
    public int maxPeople = 30; // ��������� ���������� �����
    public GameObject obstacle; // �����������, ������� ��������� ����

    private int currentPeople = 0; // ������� ���������� ����� � ����

    void Start()
    {
        UpdateCounterText();
        obstacle.SetActive(true); // ����������� �������, ���� �� ������� ������ ���������� �����
    }

    // ��������� ����� ��������
    void UpdateCounterText()
    {
        counterText.text = currentPeople + " / " + maxPeople;
    }

    // ����� �������� ������ � ����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople++;
            UpdateCounterText();

            // ���������, ������� �� ����������� ���������� �����
            if (currentPeople >= maxPeople)
            {
                AllowPassage(); // ������������ ������
            }
        }
    }

    // ����� �������� ������� �� ����
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople--;
            UpdateCounterText();
        }
    }

    // ������������� �������
    void AllowPassage()
    {
        Debug.Log("���������� ��������� ���������� �����, ����������� �����!");
        obstacle.SetActive(false); // ������� ����������� (��������, �����)
    }
}
