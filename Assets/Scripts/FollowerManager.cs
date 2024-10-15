using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public static FollowerManager Instance;
    public List<GameObject> followers = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddFollower(GameObject follower)
    {
        followers.Add(follower);
    }

    void Update()
    {
        // Определяем позицию курсора на сцене
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject follower in followers)
                {
                    // Обновляем цель для каждого человечка
                    follower.GetComponent<FollowerAgent>().SetTarget(hit.transform);
                }
            }
        }
    }
}
