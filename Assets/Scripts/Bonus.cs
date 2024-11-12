using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject followerPrefab;
    public int numberOfFollowersToAdd = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (followerPrefab == null || !followerPrefab.activeInHierarchy)
            {
                followerPrefab = GetNextAvailableFollowerPrefab();
            }

            if (followerPrefab != null)
            {
                AddFollowers();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Нет доступного префаба для спавна новых человечков!");
            }
        }
    }

    GameObject GetNextAvailableFollowerPrefab()
    {
        foreach (Rigidbody follower in CrowdManager.Instance.crowdMembers)
        {
            if (follower.gameObject.activeInHierarchy)
            {
                return follower.gameObject;
            }
        }
        return null;
    }

    private void AddFollowers()
    {
        for (int i = 0; i < numberOfFollowersToAdd; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2;
            spawnPosition.y = followerPrefab.transform.position.y;

            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
            GameObject newFollower = Instantiate(followerPrefab, spawnPosition, spawnRotation);

            CrowdManager.Instance.AddToCrowd(newFollower.GetComponent<Rigidbody>());
        }
    }
}
