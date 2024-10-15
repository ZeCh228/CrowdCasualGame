using UnityEngine;

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
}

/*using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ���� ��� ������ (���������� �����)
    public Vector3 offset; // �������� ������ ������������ ����
    public float smoothSpeed = 0.125f; // �������� ����������� �������� ������

    void LateUpdate()
    {
        if (target == null && CrowdManager.Instance.crowdMembers.Count > 0)
        {
            // ���� ������� ����� ��������, ������� ������ ������
            target = CrowdManager.Instance.crowdMembers[0].transform;
        }

        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}*/

