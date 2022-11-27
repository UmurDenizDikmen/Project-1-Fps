using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sniperr : MonoBehaviour
{
    Animator animationak;
    [Header("Settings")]
    public bool Isfire;
    public float Gunrate;
    float Rate;
    public float Range;
    public GameObject Cross;
    public GameObject Scope;
    [Header("Voice")]
    public AudioSource Soundfire;
    public AudioSource Reloadvoice;
    public AudioSource Bulletoff;
    public AudioSource Takeammo;
    [Header("Efect")]
    public ParticleSystem Efectfire;
    public ParticleSystem Bulletsign;
    public ParticleSystem Efectblood;
    [Header("Others")]
    public Camera Mycam;
    float Camfieldpov;
    public float Closerfield;
    [Header("Gunsettings")]
    int Sumbullet;
    public int Capacitybullet;
    int Remainbullet;
    public string Nameofgun;
    public TextMeshProUGUI Textsumbullet;
    public TextMeshProUGUI Textremainbullet;
    public bool Shell;
    public GameObject Blindshell;
    public GameObject Blindpoint;
    public float Powerofdamage;
    public GameObject Pointbullet;
    public GameObject Bullet;
    void Start()
    {
        Shell = true;
        Sumbullet = PlayerPrefs.GetInt(Nameofgun + "_Bullet");
        Startammoreload();
        Textsumbullet.text = Sumbullet.ToString();
        Textremainbullet.text = Remainbullet.ToString();
        animationak = GetComponent<Animator>();
        Camfieldpov = Mycam.fieldOfView;
    }
    void Startammoreload()
    {
        if (Sumbullet <= Capacitybullet)
        {
            Remainbullet = Sumbullet;
            Sumbullet = 0;
            PlayerPrefs.SetInt(Nameofgun + "_Bullet",0);
            Textsumbullet.text = Sumbullet.ToString();
            Textremainbullet.text = Remainbullet.ToString();
        }
        else
        {
            Remainbullet = Capacitybullet;
            Sumbullet -= Capacitybullet;
            PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
            Textsumbullet.text = Sumbullet.ToString();
            Textremainbullet.text = Remainbullet.ToString();
        }
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Isfire && Remainbullet !=0 && Time.time > Rate)
            {
                if (Gamecontrol.Isgamestop == false)
                {
                    Fire();
                    Rate = Gunrate + Time.time;
                }
            }
            if(Remainbullet == 0)
            {
                Bulletoff.Play();
            }
        }
           
        if (Input.GetKey(KeyCode.R))
        {
            if(Capacitybullet != Remainbullet && Sumbullet != 0)
            {
                animationak.Play("reload");
                if(Remainbullet != 0)
                {
                    if (Sumbullet <= Capacitybullet) 
                    {
                        int Valuebullet = Sumbullet + Remainbullet;
                        if (Valuebullet > Capacitybullet)
                        {
                            Remainbullet = Capacitybullet;
                            Sumbullet = Valuebullet - Capacitybullet;
                            PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
                            Textsumbullet.text = Sumbullet.ToString();
                            Textremainbullet.text = Remainbullet.ToString();
                        }
                        else
                        {
                            Remainbullet += Sumbullet;
                            Sumbullet = 0;
                            PlayerPrefs.SetInt(Nameofgun + "_Bullet", 0);
                            Textsumbullet.text = Sumbullet.ToString();
                            Textremainbullet.text = Remainbullet.ToString();
                        }
                    }
                    else
                    {
                        Sumbullet -= Capacitybullet - Remainbullet;
                        Remainbullet = Capacitybullet;
                        PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
                        Textsumbullet.text = Sumbullet.ToString();
                        Textremainbullet.text = Remainbullet.ToString();
                    }
                }
                else
                {
                    if (Sumbullet <= Capacitybullet)
                    {
                        Remainbullet = Sumbullet;
                        Sumbullet = 0;
                        PlayerPrefs.SetInt(Nameofgun + "_Bullet", 0);
                        Textsumbullet.text = Sumbullet.ToString();
                        Textremainbullet.text = Remainbullet.ToString();
                    }
                    else
                    {
                        Remainbullet = Capacitybullet;
                        Sumbullet -= Capacitybullet;
                        PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
                        Textsumbullet.text = Sumbullet.ToString();
                        Textremainbullet.text = Remainbullet.ToString();
                    }
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
          Scopecam(true);
          Scope.SetActive(true);
          Cross.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
          Scopecam(false);
          Scope.SetActive(false);
          Cross.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject control = GameObject.FindWithTag("Gamecontrol");
            control.GetComponent<Gamecontrol>().HealtUse(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            Loadammo(other.transform.gameObject.GetComponent<Ammobox>().Gunmodel, other.transform.gameObject.GetComponent<Ammobox>().Number);
            Ammocreate.Create = false;
            Destroy(other.transform.parent.gameObject);
         
        }
        if (other.gameObject.CompareTag("HealtBox"))
        {
            Loadhealt(other.transform.gameObject.GetComponent<Healtbox>().Healtnumb);
            Healtcreate.Createhealt = false;
            Destroy(other.transform.gameObject);
        }
        if (other.gameObject.CompareTag("Grenade"))
        {

            Loadgrenade(1);
            Grenadecreate.Creategrenade = false;
            Destroy(other.transform.gameObject);
        }
    }
    void Loadhealt(int Healtnumb)
    {
        Takeammo.Play();
        GameObject control = GameObject.FindWithTag("Gamecontrol");
        control.GetComponent<Gamecontrol>().Healtadd(Healtnumb);
    }
    void Loadgrenade(int Grenadenumb)
    {
        Takeammo.Play();
        GameObject control = GameObject.FindWithTag("Gamecontrol");
        control.GetComponent<Gamecontrol>().Grenadeadd(Grenadenumb);
    }
    void Loadammo(string Gunmodel,int Number)
    {
        Takeammo.Play();
        switch (Gunmodel)
        {
          case "Ak":
          PlayerPrefs.SetInt("Ak_Bullet", PlayerPrefs.GetInt("Ak_Bullet") + Number);
          break;
          case "Magnum":
          PlayerPrefs.SetInt("Magnum_Bullet",PlayerPrefs.GetInt("Magnum_Bullet")+Number);
          break;
          case "Shotgun":
          PlayerPrefs.SetInt("Shotgun_Bullet", PlayerPrefs.GetInt("Shotgun_Bullet") + Number);
          break;
          case "Sniper":
          Sumbullet += Number;
          PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
          Textsumbullet.text = Sumbullet.ToString();
          Textremainbullet.text = Remainbullet.ToString();
          break;
        }
    }
    void Reloadplay()
    {
       Reloadvoice.Play();
    }
    void Fire()
    {
        if(Shell == true)
        {
          GameObject objec = Instantiate(Blindshell, Blindpoint.transform.position, Blindpoint.transform.rotation);
          Rigidbody obj = objec.GetComponent<Rigidbody>();
          obj.AddRelativeForce(new Vector3(-10f, 1, 0f) * 25);
        }
        Instantiate(Bullet, Pointbullet.transform.position, Pointbullet.transform.rotation);
        Remainbullet--;
        Textremainbullet.text = Remainbullet.ToString();
        Soundfire.Play();
        Soundfire.Play();
        Efectfire.Play();
        animationak.Play("firesniper");
        RaycastHit hit;
        if(Physics.Raycast(Mycam.transform.position,Mycam.transform.forward,out hit, Range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(Efectblood, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.gameObject.GetComponent<Enemy>().Takedamage(Powerofdamage);
              
               
            }
            else if (hit.transform.gameObject.CompareTag("Tiltover"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 20);
            }
            else
            {
                Instantiate(Bulletsign, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
    void Scopecam(bool status)
    {
        if(status == true)
        {
            animationak.SetBool("zoom",status);
            Mycam.fieldOfView = Closerfield;
            Mycam.cullingMask = ~(1 << 6);
        }
        else
        {
            animationak.SetBool("zoom", status);
            Mycam.fieldOfView = Camfieldpov;
            Mycam.cullingMask = -1;
        }
     }
}
