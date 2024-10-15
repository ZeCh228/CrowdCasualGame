/*using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10f;
    public float height = 5f;
    public float cameraSpeed = 2f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}*/

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Цель для камеры
    public float distance = 10f; // Расстояние от камеры до цели
    public float height = 5f; // Высота камеры относительно цели
    public float cameraSpeed = 2f; // Скорость сглаживания камеры

    void LateUpdate()
    {
        if (target == null && CrowdManager.Instance.crowdMembers.Count > 0)
        {
            // Если текущий лидер погибает, переключаемся на следующего персонажа в толпе
            target = CrowdManager.Instance.crowdMembers[0].transform;
        }

        if (target != null)
        {
            // Рассчитываем желаемую позицию камеры
            Vector3 desiredPosition = target.position + new Vector3(0, height, -distance);
            // Плавно перемещаем камеру к новой позиции
            transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
            // Камера всегда смотрит на цель
            transform.LookAt(target);
        }
    }
}


