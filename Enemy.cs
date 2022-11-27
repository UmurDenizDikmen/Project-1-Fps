using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public float Healt;
    public float Powerofattcak;
    GameObject control;
    Animator Animator;
    void Start()
    {
       control = GameObject.FindWithTag("Gamecontrol");
       Animator = GetComponent<Animator>();
    }
    public  void Takedamage(float Powerofdamage)
    {
        Healt -= Powerofdamage;
        if(Healt <= 0)
        {
            Death();
            gameObject.tag = "Untagged"; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            control.GetComponent<Gamecontrol>().Healtbarattack(Powerofattcak);
            Death();
        }
    }
    public void Death()
    {
        control.GetComponent<Gamecontrol>().Updatenemynumber();
        Destroy(gameObject,3f);
        Animator.SetTrigger("Death");
    }
}
    


