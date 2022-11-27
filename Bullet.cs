using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rg;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        rg.velocity = transform.forward * 250f;
        Destroy(gameObject,2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
  
}
