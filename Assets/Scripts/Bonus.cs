/*using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject followerPrefab; // Префаб для нового человечка
    public int numberOfFollowersToAdd = 3; // Количество новых персонажей

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что это игрок
        {
            AddFollowers();
            Destroy(gameObject); // Удаляем бонус-куб после использования
        }
    }

    void AddFollowers()
    {
        for (int i = 0; i < numberOfFollowersToAdd; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2;
            spawnPosition.y = followerPrefab.transform.position.y; // Чтобы персонажи не проваливались под землю

            // Создаём нового человечка с заданным поворотом (90, 0, 0)
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // Устанавливаем нужную ротацию
            GameObject newFollower = Instantiate(followerPrefab, spawnPosition, spawnRotation);

            CrowdManager.Instance.AddToCrowd(newFollower.GetComponent<Rigidbody>());
        }
    }
}*/


using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject followerPrefab; // Префаб для нового человечка
    public int numberOfFollowersToAdd = 3; // Количество новых персонажей

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что это игрок
        {
            if (followerPrefab == null || !followerPrefab.activeInHierarchy)
            {
                // Если текущий префаб недоступен (мертв или неактивен), ищем новый
                followerPrefab = GetNextAvailableFollowerPrefab();
            }

            if (followerPrefab != null) // Убедимся, что есть доступный префаб
            {
                AddFollowers();
                Destroy(gameObject); // Удаляем бонус-куб после использования
            }
            else
            {
                Debug.LogError("Нет доступного префаба для спавна новых человечков!");
            }
        }
    }

    // Метод для получения следующего доступного префаба
    GameObject GetNextAvailableFollowerPrefab()
    {
        // Проходим по всем оставшимся персонажам
        foreach (Rigidbody follower in CrowdManager.Instance.crowdMembers)
        {
            if (follower.gameObject.activeInHierarchy) // Проверяем, что персонаж активен
            {
                return follower.gameObject; // Возвращаем активного персонажа
            }
        }
        return null; // Если все персонажи мертвы или неактивны, возвращаем null
    }

    void AddFollowers()
    {
        for (int i = 0; i < numberOfFollowersToAdd; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2;
            spawnPosition.y = followerPrefab.transform.position.y; // Чтобы персонажи не проваливались под землю

            // Создаём нового человечка с заданным поворотом (90, 0, 0)
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // Устанавливаем нужную ротацию
            GameObject newFollower = Instantiate(followerPrefab, spawnPosition, spawnRotation);

            CrowdManager.Instance.AddToCrowd(newFollower.GetComponent<Rigidbody>());
        }
    }
}
