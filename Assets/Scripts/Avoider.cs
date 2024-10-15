using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Avoider : MonoBehaviour
{
    [SerializeField] private float _repulceStregth;

    private Rigidbody rb;

    public void Construct (bool isLeader)
    { 
        if (isLeader)
            this.enabled = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Vector3 avoid = this.transform.position - other.transform.position;
            rb.AddForce(avoid.normalized * _repulceStregth);
        }
    }
}
