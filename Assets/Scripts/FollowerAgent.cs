using UnityEngine;

public class FollowerAgent : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1f; // ����������� ���������� �� ������ �������

    private Transform targetPosition; // ������� ��� ���������� (��������, �� ��������)

    void Start()
    {
        // ��������� ���� � ��� ������� ������� �������
        targetPosition = Camera.main.transform;
    }

    void Update()
    {
        AvoidOthers();
        MoveTowardsTarget();      
    }

    void MoveTowardsTarget()
    {
        // �������� ������ �������� � ����
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition.position);

        // ���� ���������� �� ���� ������ ������������ � ���������
        if (distance > minDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // ��������� ���� (��������, ���� ���� ��������)
    public void SetTarget(Transform newTarget)
    {
        targetPosition = newTarget;
    }

    void AvoidOthers()
    {
        foreach (GameObject follower in FollowerManager.Instance.followers)
        {
            if (follower != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, follower.transform.position);
                if (distance < minDistance)
                {
                    // ������������� �� ������� ���������
                    Vector3 repulsion = transform.position - follower.transform.position;
                    transform.position += repulsion.normalized * (minDistance - distance) * Time.deltaTime;
                }
            }
        }
    }
}
