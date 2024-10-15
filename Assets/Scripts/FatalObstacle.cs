using UnityEngine;

public class FatalObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект, вошедший в триггер, имеет тег Player
        if (other.CompareTag("Player"))
        {
            // Удаляем персонажа из толпы
            RemoveFromCrowd(other.gameObject);
        }
    }

    void RemoveFromCrowd(GameObject character)
    {
        // Удаляем персонажа из списка толпы в CrowdManager
        if (CrowdManager.Instance.crowdMembers.Contains(character))
        {
            CrowdManager.Instance.crowdMembers.Remove(character);
        }

        // Уничтожаем персонажа (можно добавить анимацию исчезновения или эффект)
        Destroy(character);
    }
}
