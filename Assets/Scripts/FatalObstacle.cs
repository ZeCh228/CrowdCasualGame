/*using UnityEngine;

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
        if (CrowdManager.Instance.crowdMembers.Contains(character.GetComponent<Rigidbody>()))
        {
            CrowdManager.Instance.crowdMembers.Remove(character.GetComponent<Rigidbody>());
        }

        // Уничтожаем персонажа (можно добавить анимацию исчезновения или эффект)
        Destroy(character);
    }
}
*/

using UnityEngine;

public class FatalObstacle : MonoBehaviour
{
    // Когда персонаж входит в триггер
    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это персонаж (например, по тегу "Player")
        if (other.CompareTag("Player"))
        {
            // Вызываем метод удаления персонажа из CrowdManager
            CrowdManager.Instance.RemoveFromCrowd(other.gameObject);
        }
    }
}
