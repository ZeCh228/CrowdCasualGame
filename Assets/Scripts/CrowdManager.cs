/*using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    // Реализация Singleton
    public static CrowdManager Instance;

    public float speed = 5f; // Скорость движения толпы
    public float followDistance = 1.5f; // Минимальное расстояние между членами толпы
    public List<GameObject> crowdMembers = new List<GameObject>(); // Список всех персонажей в толпе

    void Awake()
    {
        // Проверка на существование экземпляра Singleton
        if (Instance == null)
        {
            Instance = this; // Назначаем текущий экземпляр
        }
        else
        {
            Destroy(gameObject); // Удаляем дублирующий экземпляр, если он есть
        }
    }

    void Update()
    {
        // Если нажата левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Определяем позицию курсора
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Двигаем первого персонажа к курсору
                MoveToTarget(crowdMembers[0], hit.point);

                // Заставляем остальные элементы следовать за предыдущими
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i], crowdMembers[i - 1].transform.position);
                    AvoidOthers(crowdMembers[i]); // Проверяем на пересечение с другими членами толпы
                }
            }
        }
    }

    // Функция для перемещения персонажа к цели
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position).normalized;
        float distance = Vector3.Distance(follower.transform.position, targetPosition);

        // Движение к цели только если дистанция больше минимальной
        if (distance > followDistance)
        {
            follower.transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Метод для избегания пересечений с другими персонажами
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

                    // Ограничиваем силу отталкивания
                    float repulsionStrength = Mathf.Clamp(followDistance - distance, 0, 1);

                    // Применяем отталкивание только в плоскости XZ
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repulsionStrength * Time.deltaTime;

                    follower.transform.position += repulsionForce;
                }
            }
        }
    }


    // Добавление нового члена в толпу
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
    public float speed = 5f; // Скорость движения толпы
    public float followDistance = 1.5f; // Минимальное расстояние между членами толпы
    public List<Rigidbody> crowdMembers = new List<Rigidbody>(); // Список всех персонажей в толпе
    public static CrowdManager Instance; // Singleton для глобального доступа

    [SerializeField] private float repuls;
    private EntityCounter entityCounter;
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
        entityCounter = FindObjectOfType<EntityCounter>();
    }

    public bool IsLeader(GameObject member)
    {
        // Логика проверки, является ли член толпы лидером
        return false;
    }

    void Update()
    {
        if (crowdMembers.Count == 0) return; // Проверка на наличие членов толпы

        // Проверяем, удерживается ли левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Определяем позицию курсора
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                target = new Vector3(target.x, 0, target.z);

                // Двигаем первого персонажа к курсору
                if (crowdMembers.Count > 0)
                {
                    MoveToTarget(crowdMembers[0].gameObject, target);
                }

                if (isNeedToStopCrowd) return;

                // Заставляем остальных следовать за предыдущими членами толпы
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i].gameObject, target);
                }
            }
        }
        else if (target != Vector3.zero)
        {
            // Проверка, что толпа не пуста, чтобы двигать персонажей
            if (crowdMembers.Count > 0)
            {
                MoveToTarget(crowdMembers[0].gameObject, target);

                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i].gameObject, target);
                }
            }
        }


    }

    // Функция для перемещения персонажа к цели
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position);
        direction.y = 0;
        float distance = Vector3.Distance(follower.transform.position, new Vector3(targetPosition.x, follower.transform.position.y, targetPosition.z));

        if (distance > followDistance)
        {
            isNeedToStopCrowd = false;
            follower.GetComponent<Rigidbody>().velocity = direction.magnitude > 1 ? direction.normalized * speed : direction * speed;
        }
        else if (follower == crowdMembers[0] && distance <= 0.1f)
        {
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else if (follower != crowdMembers[0] && distance <= CalculateDistance())
        {
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private float CalculateDistance()
    {
        if (crowdMembers.Count <= 7) return 1.5f;
        else if (crowdMembers.Count <= 14) return 30f;
        else if (crowdMembers.Count <= 35) return 80f;

        return 0.1f;
    }

    // Метод для предотвращения столкновений с другими персонажами
    void AvoidOthers(GameObject follower)
    {
        foreach (Rigidbody member in crowdMembers)
        {
            if (member.gameObject != follower)
            {
                float distance = Vector3.Distance(follower.transform.position, member.transform.position);
                if (distance < followDistance)
                {
                    Vector3 repulsion = (follower.transform.position - member.transform.position).normalized;
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repuls;
                    follower.GetComponent<Rigidbody>().velocity += repulsionForce;
                }
            }
        }
    }

    // Добавление нового члена в толпу
    public void AddToCrowd(Rigidbody newMember)
    {
        crowdMembers.Add(newMember);
        entityCounter.UpdateEntityCount(); // Обновляем счетчик после добавления
    }

    // Удаление персонажа из толпы
    public void RemoveFromCrowd(GameObject character)
    {
        Rigidbody rb = character.GetComponent<Rigidbody>();

        if (crowdMembers.Contains(rb))
        {
            crowdMembers.Remove(rb);
            entityCounter.UpdateEntityCount();
            Debug.Log("Персонаж удален. Осталось персонажей: " + crowdMembers.Count);

            // После удаления персонажа проверяем на проигрыш
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.CheckForGameOver();
            }
        }

        Destroy(character);
    }
}

