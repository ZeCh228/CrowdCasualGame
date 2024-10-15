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
    Vector3 target = Vector3.zero;

    bool isNeedToStopCrowd = false;
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

        // crowdMembers[0].GetComponent<Avoider>().Construct(true);
    }
    public bool IsLeader(GameObject member)
    {
        //�������� ����� ���� ���  ����� �������� ����� ������
        return false;
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
                target = hit.point;

                target = new Vector3(target.x, 0, target.z);
                // ������� ������� ��������� � �������
                MoveToTarget(crowdMembers[0].gameObject, target);
                if (isNeedToStopCrowd)
                {
                    return;
                }
                // ���������� ��������� ��������� �� ����������� ������� �����
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i].gameObject, target);
                    //     AvoidOthers(crowdMembers[i].gameObject); // �������� ������������ � �������
                }
            }
        }

        else if (target != Vector3.zero)
        {
            MoveToTarget(crowdMembers[0].gameObject, target);
            // ��� ������� ������������ ����� ����� ��������� ���� ���������� �� ����������
            for (int i = 1; i < crowdMembers.Count; i++)
            {
                MoveToTarget(crowdMembers[i].gameObject, target);
            }

        }
    }

    // ������� ��� ����������� ��������� � ����
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position);
        direction.y = 0;
        float distance = Vector3.Distance(follower.transform.position, new Vector3(targetPosition.x, follower.transform.position.y, targetPosition.z));

        print("dist " + distance);
        // ������� ��������� ������ ���� ��������� ������ �����������

        if (distance > followDistance)
        {
            isNeedToStopCrowd = false;

            //follower.transform.position += direction * speed * Time.deltaTime;
            follower.GetComponent<Rigidbody>().velocity = direction.magnitude > 1 ? direction.normalized * speed : direction * speed;
            //print("Magn " + follower.GetComponent<Rigidbody>().velocity.magnitude);
        }
        else if (follower == crowdMembers[0] && distance <= 0.1f)//����� ���� ������, ����� ����������� ���������� ������
        {/*
            if (follower == crowdMembers[0].gameObject)
            {
                //isNeedToStopCrowd = true;
            }*/
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else if (follower != crowdMembers[0] && distance <= CalculateDistance())//����� ���� ������, ����� ����������� ���������� ������
        {/*
            if (follower == crowdMembers[0].gameObject)
            {
                //isNeedToStopCrowd = true;
            }*/
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    private float CalculateDistance()
    {
        if (crowdMembers.Count <= 7)
        {
            return 1.5f;
        }
        else if (crowdMembers.Count <= 14)
        {
            return 30f;
        }
        else if (crowdMembers.Count <= 35)
        {
            return 80f;
        }

        return 0.1f;
    }
    // ����� ��� �������������� ������������ � ������� �����������
    void AvoidOthers(GameObject follower)
    {
        foreach (Rigidbody member in crowdMembers)
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
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repuls; /** Time.deltaTime*/

                    //follower.transform.position += repulsionForce;
                    follower.GetComponent<Rigidbody>().velocity += repulsionForce;
                }
            }
        }
    }

    // ���������� ������ ����� � �����
    public void AddToCrowd(Rigidbody newMember)
    {
        crowdMembers.Add(newMember);
    }
}
