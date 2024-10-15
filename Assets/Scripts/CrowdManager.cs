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
    //public List<GameObject> crowdMembers = new List<GameObject>(); // Список всех персонажей в толпе
    public List<Rigidbody> crowdMembers = new List<Rigidbody>(); // Список всех персонажей в толпе

    public static CrowdManager Instance; // Singleton для глобального доступа

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
        // Проверяем, удерживается ли левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Определяем позицию курсора
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Двигаем первого персонажа к курсору
                MoveToTarget(crowdMembers[0], hit.point);

                // Заставляем остальных следовать за предыдущими членами толпы
                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i], crowdMembers[i - 1].transform.position);
                    AvoidOthers(crowdMembers[i]); // Избегаем столкновений с другими
                }
            }
        }
    }

    // Функция для перемещения персонажа к цели
    void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position).normalized;
        float distance = Vector3.Distance(follower.transform.position, targetPosition);

        // Двигаем персонажа только если дистанция больше минимальной
        if (distance > followDistance)
        {
            follower.transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Метод для предотвращения столкновений с другими персонажами
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

                    // Отталкивание только в плоскости XZ
                    //Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repulsionStrength * Time.deltaTime;
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repuls * Time.deltaTime;

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
