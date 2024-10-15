using UnityEngine;

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
}
