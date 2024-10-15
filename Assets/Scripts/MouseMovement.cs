using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // ���������, ������������ �� ����� ������ ����
        if (Input.GetMouseButton(0))
        {
            // �������� ������� ������� � ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ������� ��� � ���������, ��� �� ����� � ������ ���������
            if (Physics.Raycast(ray, out hit))
            {
                // ����� ������ ���������� X � Z ��� �������� � ���������
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                isMoving = true;
            }
        }
        else
        {
            // ������������� ��������, ���� ������ ��������
            isMoving = false;
        }

        // ���� �������� ���������, ���������� ��� � ����
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // ���������� ��������� � ������� ������� � ��������� XZ
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}

