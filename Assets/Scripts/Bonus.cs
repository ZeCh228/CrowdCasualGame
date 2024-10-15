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

            GameObject newFollower = Instantiate(followerPrefab, spawnPosition, Quaternion.identity);
            CrowdManager.Instance.AddToCrowd(newFollower.GetComponent<Rigidbody>());
        }
    }
}
