using TMPro;
using UnityEngine;

public class PassageZone : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Текстовый элемент для отображения счётчика
    public int maxPeople = 30; // Требуемое количество людей
    public GameObject obstacle; // Препятствие, которое блокирует путь

    private int currentPeople = 0; // Текущее количество людей в зоне

    void Start()
    {
        UpdateCounterText();
        obstacle.SetActive(true); // Препятствие активно, пока не набрано нужное количество людей
    }

    // Обновляем текст счётчика
    void UpdateCounterText()
    {
        counterText.text = currentPeople + " / " + maxPeople;
    }

    // Когда персонаж входит в зону
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople++;
            UpdateCounterText();

            // Проверяем, набрано ли достаточное количество людей
            if (currentPeople >= maxPeople)
            {
                AllowPassage(); // Разблокируем проход
            }
        }
    }

    // Когда персонаж выходит из зоны
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople--;
            UpdateCounterText();
        }
    }

    // Разблокировка прохода
    void AllowPassage()
    {
        Debug.Log("Достигнуто требуемое количество людей, препятствие снято!");
        obstacle.SetActive(false); // Убираем препятствие (например, дверь)
    }
}
