using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberDebuger : MonoBehaviour
{
    [SerializeField] Color color = Color.red;
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        if (rb == null) 
        {
            rb = GetComponent<Rigidbody>();
        }

        // ����� ���� (������ �������� �� �������������)
        float rayLength = 5.0f;

        // ��������� ����

        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, rb.velocity);
    }
}
