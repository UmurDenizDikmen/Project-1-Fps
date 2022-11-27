using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Gamecontrol : MonoBehaviour
{
    [Header("Gunsettings")]
    public GameObject[] Guns;
    public AudioSource Gunchange;
    public GameObject Granade;
    public GameObject Pointgranade;
    public Camera Mycam;
    [Header("Enemysettings")]
    public GameObject [] Enemypoint;
    public GameObject[] Enemies;
    public GameObject[] Targetpoint;
    public int Sumenemynumber;
    public static  int Remainenemynumber;
    public TextMeshProUGUI Remainenemytext;
    public float Timeofenemy;
    [Header("Healtsettings")]
    public Image Healtbar;
    float Healt = 100;
    [Header("Othersettings")]
    public GameObject Gameoverpanel;
    public GameObject Youwinpanel;
    public GameObject Pausepanel;
    public AudioSource Gamemusic;
    public TextMeshProUGUI Healttext;
    public TextMeshProUGUI Grenadetext;
    public static bool Isgamestop;
    void Start()
    {
        Beginingvalue();
        Gamemusic.Play();
        StartCoroutine(Enemycreate());
    }
    IEnumerator Enemycreate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Timeofenemy);
            if (Sumenemynumber!= 0)
            {
                int indexenemy = Random.Range(0, 4);
                int indexpoint = Random.Range(0, 2);
                int indextarget = Random.Range(0, 2);
                GameObject obj = Instantiate(Enemies[indexenemy], Enemypoint[indexpoint].transform.position, Quaternion.identity);
                obj.GetComponent<NavMeshAgent>().SetDestination(Targetpoint[indextarget].transform.position);
                Sumenemynumber--;
            }
        } 
    }
    void Beginingvalue()
    {

        PlayerPrefs.SetInt("Ak_Bullet", 70);
        PlayerPrefs.SetInt("Magnum_Bullet", 30);
        PlayerPrefs.SetInt("Shotgun_Bullet", 50);
        PlayerPrefs.SetInt("Sniper_Bullet", 20);
        PlayerPrefs.SetInt("Healt_Numb", 1);
        PlayerPrefs.SetInt("Grenade_Numb", 5);
        Isgamestop = false;
        if (!PlayerPrefs.HasKey("Gamestart"))
        {
            PlayerPrefs.SetInt("Ak_Bullet", 70);
            PlayerPrefs.SetInt("Magnum_Bullet", 30);
            PlayerPrefs.SetInt("Shotgun_Bullet", 50);
            PlayerPrefs.SetInt("Sniper_Bullet", 20);
            PlayerPrefs.SetInt("Healt_Numb", 1);
            PlayerPrefs.SetInt("Grenade_Numb", 5);
            PlayerPrefs.SetInt("Gamestart", 1);
        }
        Healttext.text = PlayerPrefs.GetInt("Healt_Numb").ToString();
        Grenadetext.text = PlayerPrefs.GetInt("Grenade_Numb").ToString();
        Remainenemytext.text = Sumenemynumber.ToString();
        Remainenemynumber = Sumenemynumber;
    }
    public void Updatenemynumber()
    {
        Remainenemynumber--;
        if(Remainenemynumber <= 0)
        {
            Youwinpanel.SetActive(true);
            Remainenemytext.text = "0";
            Time.timeScale = 0;
            Isgamestop = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject Fpsac = GameObject.FindWithTag("Player");
            Fpsac.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        }
        else
        {
            Remainenemytext.text = Remainenemynumber.ToString();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(Grenadetext.text != "0")
            {
                GameObject gra = Instantiate(Granade, Pointgranade.transform.position, Pointgranade.transform.rotation);
                Vector3 myangle = Quaternion.AngleAxis(90, Mycam.transform.forward) * Mycam.transform.forward;
                gra.GetComponent<Rigidbody>().AddForce(myangle * 250);
                GrenadeUse(1);
            }
          
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)&& Isgamestop == false)
        {
            Gunchange.Play();
            foreach (GameObject Gun in Guns)
            {
              Gun.SetActive(false);
            }
            Guns[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Isgamestop == false)
        {
            Gunchange.Play();
            foreach (GameObject Gun in Guns)
            {
             Gun.SetActive(false);
            }
            Guns[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Isgamestop == false)
        {
            Gunchange.Play();
            foreach (GameObject Gun in Guns)
            {
             Gun.SetActive(false);
            }
            Guns[2].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Isgamestop == false)
        {
            Gunchange.Play();
            foreach (GameObject Gun in Guns)
            {
             Gun.SetActive(false);
            }
            Guns[3].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& Isgamestop == false)
        {
            Pause();
        }
    }
    public void Healtbarattack(float powerofattcak)
    {
        Healt -= powerofattcak;
        Healtbar.fillAmount = Healt / 100;
        if(Healt <= 0)
        {
            Gameover();
        }
    }
    public void Healtadd(int Healtnumb)
    {
        PlayerPrefs.SetInt("Healt_Numb", PlayerPrefs.GetInt("Healt_Numb")+ Healtnumb);
        Healttext.text = PlayerPrefs.GetInt("Healt_Numb").ToString();
    }
    public void HealtUse(int Healtuse)
    {
        if (PlayerPrefs.GetInt("Healt_Numb") != 0 && Healt != 100)
        {
            Healt += Healtuse;
            Healtbar.fillAmount = Healt / 100;
            PlayerPrefs.SetInt("Healt_Numb", PlayerPrefs.GetInt("Healt_Numb") - Healtuse);
            Healttext.text = PlayerPrefs.GetInt("Healt_Numb").ToString();
        }
    }
    public void Grenadeadd(int Grenadenumb)
    {
        PlayerPrefs.SetInt("Grenade_Numb", PlayerPrefs.GetInt("Grenade_Numb") + Grenadenumb);
        Grenadetext.text = PlayerPrefs.GetInt("Grenade_Numb").ToString();
    }
    public void GrenadeUse(int Grenadeuse)
    {
        if (PlayerPrefs.GetInt("Grenade_Numb") != 0)
        {
            PlayerPrefs.SetInt("Grenade_Numb", PlayerPrefs.GetInt("Grenade_Numb") - Grenadeuse);
            Grenadetext.text = PlayerPrefs.GetInt("Grenade_Numb").ToString();
        }
    }
    void Gameover()
    {
        Gameoverpanel.SetActive(true);
        Time.timeScale = 0;
        Isgamestop = true;
        Cursor.visible = true;
        GameObject Fpsac = GameObject.FindWithTag("Player");
        Fpsac.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Playagain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Isgamestop = false;
        Cursor.visible = false;
        GameObject Fpsac = GameObject.FindWithTag("Player");
        Fpsac.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Pausepanel.SetActive(true);
        Time.timeScale = 0;
        Isgamestop = true;
        Cursor.visible = true;
        GameObject Fpsac = GameObject.FindWithTag("Player");
        Fpsac.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Continue()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
        Isgamestop = false;
        Cursor.visible = false;
        GameObject Fpsac = GameObject.FindWithTag("Player");
        Fpsac.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
