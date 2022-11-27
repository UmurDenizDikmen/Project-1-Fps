using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shotgunn : MonoBehaviour
{
    Animator animationak;
    [Header("Settings")]
    public bool Isfire;
    public float Gunrate;
    float Rate;
    public float Range;
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
    bool Iszoom;
    bool Isclose;
    public float Powerofdamage;
    public GameObject []Pointbullet;
    public GameObject []Bullet;
    void Start()
    {
        Shell = false;
        Sumbullet = PlayerPrefs.GetInt(Nameofgun + "_Bullet");
        Startammoreload();
        Textsumbullet.text = Sumbullet.ToString();
        Textremainbullet.text = Remainbullet.ToString();
        animationak = GetComponent<Animator>();
        Mycam.fieldOfView = 60;
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
        if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            if(Isfire && Remainbullet !=0 && Time.time > Rate)
            {
                if (Gamecontrol.Isgamestop == false)
                {
                    Fire(false);
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
                animationak.Play("Reload");
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
            Iszoom = true;
            animationak.SetBool("zoom", true);
           
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animationak.SetBool("zoom", false);
            Mycam.fieldOfView = 60;
            Iszoom = false;
        }
        if(Iszoom == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Isfire && Remainbullet != 0 && Time.time > Rate)
                {
                    Fire(true);
                    Rate = Gunrate + Time.time;
                }
                if (Remainbullet == 0)
                {
                    Bulletoff.Play();
                }
            }
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

    IEnumerator Camerashake(float vibration,float magnitude)
    {
        Vector3 originalposition = Mycam.transform.localPosition;
        float timee = 0.0f;
        while(timee < vibration)
        {
            float x= Random.Range(-1f, 1f) * magnitude;
            Mycam.transform.localPosition = new Vector3(x,originalposition.y,originalposition.z);
            timee += Time.deltaTime;
            yield return null;
        }
        Mycam.transform.localPosition = originalposition;
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
          Sumbullet += Number;
          PlayerPrefs.SetInt(Nameofgun + "_Bullet", Sumbullet);
          Textsumbullet.text = Sumbullet.ToString();
          Textremainbullet.text = Remainbullet.ToString();
          break;
          case "Sniper":
          PlayerPrefs.SetInt("Sniper_Bullet", PlayerPrefs.GetInt("Sniper_Bullet") + Number);
          break;
        }
    }
    void Reloadplay()
    {
       Reloadvoice.Play();
    }
    void Fire(bool Isclose)
    {
        StartCoroutine(Camerashake(.10f, .2f));
        if(Shell == true)
        {
          GameObject objec = Instantiate(Blindshell, Blindpoint.transform.position, Blindpoint.transform.rotation);
          Rigidbody obj = objec.GetComponent<Rigidbody>();
          obj.AddRelativeForce(new Vector3(-10f, 1, 0f) * 25);
        }
        Instantiate(Bullet[0], Pointbullet[0].transform.position, Pointbullet[0].transform.rotation);
        Instantiate(Bullet[1], Pointbullet[1].transform.position, Pointbullet[1].transform.rotation);
        Remainbullet--;
        Textremainbullet.text = Remainbullet.ToString();
        Soundfire.Play();
        Efectfire.Play();
        if(Isclose == false)
        {
          animationak.Play("fireshotgun");
        }
        else
        {
          animationak.Play("zoomfire");
        }
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
                rg.AddForce(-hit.normal * 60);
            }
            else
            {
                Instantiate(Bulletsign, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
    public void Scopecam()
    {
        Mycam.fieldOfView = Closerfield;
    }
}
