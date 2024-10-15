using TMPro;
using UnityEngine;

public class EntityCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Ссылка на TMP текстовый элемент

    void Start()
    {
        UpdateEntityCount(); // Обновляем счетчик при старте
    }

    // Этот метод будет вызываться для обновления счетчика
    public void UpdateEntityCount()
    {
        if (CrowdManager.Instance != null)
        {
            int count = CrowdManager.Instance.crowdMembers.Count;
            counterText.text = count.ToString(); // Обновляем текст только количеством
        }
    }
}
