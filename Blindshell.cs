using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindshell : MonoBehaviour
{
    AudioSource Blindshellvoice;
    void Start()
    {
        Blindshellvoice = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            Blindshellvoice.Play();
            if (Blindshellvoice.isPlaying)
            {
                Destroy(gameObject, 2f);
            }
        }
    }
    void Update()
    {
        
    }
}
