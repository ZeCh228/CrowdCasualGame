/*using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    // ���������� Singleton
    public static CrowdManager Instance;

    public float speed = 5f; // �������� �������� �����
    public float followDistance = 1.5f; // ����������� ���������� ����� ������� �����
    public List<GameObject> crowdMembers = new List<GameObject>(); // ������ ���� ���������� � �����

    void Awake()
    {
        // �������� �� ������������� ���������� Singleton
        if (Instance == null)
        {
            Instance = this; // ��������� ������� ���������
        }
        else
        {
            Destroy(gameObject); // ������� ����������� ���������, ���� �� ����
        }
    }

    void Update()
    {
        // ���� ������ ����� ������ ����
        if (Input.GetMouseButton(0))
        {
            // ���������� ������� �������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // ������� ������� ��������� � �������
                MoveToTarget(crowdMembers[0], hit.point);

                // ���������� ��������� �������� ��������� �� �����������
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i], crowdMembers[i - 1].transform.position);
                    AvoidOthers(crowdMembers[i]); // ��������� �� ����������� � ������� ������� �����
                }
            }
        }
    }

    // ������� ��� ����������� ��������� � ����
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position).normalized;
        float distance = Vector3.Distance(follower.transform.position, targetPosition);

        // �������� � ���� ������ ���� ��������� ������ �����������
        if (distance > followDistance)
        {
            follower.transform.position += direction * speed * Time.deltaTime;
        }
    }

    // ����� ��� ��������� ����������� � ������� �����������
    *//* void AvoidOthers(GameObject follower)
     {
         foreach (GameObject member in crowdMembers)
         {
             if (member != follower)
             {
                 float distance = Vector3.Distance(follower.transform.position, member.transform.position);
                 if (distance < followDistance)
                 {
                     Vector3 repulsion = (follower.transform.position - member.transform.position).normalized;
                     follower.transform.position += repulsion * (followDistance - distance) * Time.deltaTime;
                 }
             }
         }
     }*//*

    void AvoidOthers(GameObject follower)
    {
        foreach (GameObject member in crowdMembers)
        {
            if (member != follower)
            {
                float distance = Vector3.Distance(follower.transform.position, member.transform.position);
                if (distance < followDistance)
                {
                    Vector3 repulsion = (follower.transform.position - member.transform.position).normalized;

                    // ������������ ���� ������������
                    float repulsionStrength = Mathf.Clamp(followDistance - distance, 0, 1);

                    // ��������� ������������ ������ � ��������� XZ
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repulsionStrength * Time.deltaTime;

                    follower.transform.position += repulsionForce;
                }
            }
        }
    }


    // ���������� ������ ����� � �����
    public void AddToCrowd(GameObject newMember)
    {
        crowdMembers.Add(newMember);
    }
}
*/






using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public float speed = 5f; // �������� �������� �����
    public float followDistance = 1.5f; // ����������� ���������� ����� ������� �����
    //public List<GameObject> crowdMembers = new List<GameObject>(); // ������ ���� ���������� � �����
    public List<Rigidbody> crowdMembers = new List<Rigidbody>(); // ������ ���� ���������� � �����

    public static CrowdManager Instance; // Singleton ��� ����������� �������

    [SerializeField] private float repuls;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // ���������, ������������ �� ����� ������ ����
        if (Input.GetMouseButton(0))
        {
            // ���������� ������� �������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // ������� ������� ��������� � �������
                MoveToTarget(crowdMembers[0], hit.point);

                // ���������� ��������� ��������� �� ����������� ������� �����
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i], crowdMembers[i - 1].transform.position);
                    AvoidOthers(crowdMembers[i]); // �������� ������������ � �������
                }
            }
        }
    }

    // ������� ��� ����������� ��������� � ����
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position).normalized;
        float distance = Vector3.Distance(follower.transform.position, targetPosition);

        // ������� ��������� ������ ���� ��������� ������ �����������
        if (distance > followDistance)
        {
            follower.transform.position += direction * speed * Time.deltaTime;
        }
    }

    // ����� ��� �������������� ������������ � ������� �����������
    void AvoidOthers(GameObject follower)
    {
        foreach (GameObject member in crowdMembers)
        {
            if (member != follower)
            {
                float distance = Vector3.Distance(follower.transform.position, member.transform.position);
                if (distance < followDistance)
                {
                    Vector3 repulsion = (follower.transform.position - member.transform.position).normalized;
                    //float repulsionStrength = Mathf.Clamp(followDistance - distance, 0, 1);

                    // ������������ ������ � ��������� XZ
                    //Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repulsionStrength * Time.deltaTime;
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repuls * Time.deltaTime;

                    follower.transform.position += repulsionForce;
                }
            }
        }
    }

    // ���������� ������ ����� � �����
    public void AddToCrowd(GameObject newMember)
    {
        crowdMembers.Add(newMember);
    }
}
