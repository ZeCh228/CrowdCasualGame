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
        // ���������� ������� ������� �� �����
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                foreach (GameObject follower in followers)
                {
                    // ��������� ���� ��� ������� ���������
                    follower.GetComponent<FollowerAgent>().SetTarget(hit.transform);
                }
            }
        }
    }
}
