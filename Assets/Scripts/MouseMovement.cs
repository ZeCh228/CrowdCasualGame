using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // Проверяем, удерживается ли левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Получаем позицию курсора в мире
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Пускаем луч и проверяем, что он попал в нужную плоскость
            if (Physics.Raycast(ray, out hit))
            {
                // Берем только координаты X и Z для движения в плоскости
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                isMoving = true;
            }
        }
        else
        {
            // Останавливаем движение, если кнопка отпущена
            isMoving = false;
        }

        // Если персонаж двигается, направляем его к цели
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // Перемещаем персонажа к целевой позиции в плоскости XZ
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}

