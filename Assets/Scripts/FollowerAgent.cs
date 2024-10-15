using UnityEngine;

public class FollowerAgent : MonoBehaviour
{
    public float speed = 5f;
    public float minDistance = 1f; // ћинимальное рассто€ние до других агентов

    private Transform targetPosition; // ѕозици€ дл€ следовани€ (например, за курсором)

    void Start()
    {
        // Ќачальна€ цель Ч это текуща€ позици€ курсора
        targetPosition = Camera.main.transform;
    }

    void Update()
    {
        AvoidOthers();
        MoveTowardsTarget();      
    }

    void MoveTowardsTarget()
    {
        // ѕолучаем вектор движени€ к цели
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition.position);

        // ≈сли рассто€ние до цели больше минимального Ч двигаемс€
        if (distance > minDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // ќбновл€ем цель (например, если цель мен€етс€)
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
                    // ќтталкиваемс€ от другого человечка
                    Vector3 repulsion = transform.position - follower.transform.position;
                    transform.position += repulsion.normalized * (minDistance - distance) * Time.deltaTime;
                }
            }
        }
    }
}
