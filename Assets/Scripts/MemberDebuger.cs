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

        // Длина луча (можешь изменить по необходимости)
        float rayLength = 5.0f;

        // Отрисовка луча

        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, rb.velocity);
    }
}
