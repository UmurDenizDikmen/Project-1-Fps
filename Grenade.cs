using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float Powerofgrenade;
    public float Range;
    public float Uppower;
    public ParticleSystem Explosioneffect;
    public AudioSource Explosionvoice;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            Destroy(gameObject, .3f);
            Explosin();
        }
    }
    void Explosin()
    {
        Vector3 Exp = transform.position;
        GameObject bomb = Instantiate(Explosioneffect, transform.position, transform.rotation).gameObject;
        Destroy(bomb,2f);
        Explosionvoice.Play();
        Collider[] colliders = Physics.OverlapSphere(Exp,Range);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(hit != null&& rb)
            {
                if (hit.gameObject.CompareTag("Enemy"))
                {
                    hit.transform.gameObject.GetComponent<Enemy>().Death();
                }
                rb.AddExplosionForce(Powerofgrenade, Exp,Range,Uppower,ForceMode.Impulse);
            }

        }
    }
} 
   
