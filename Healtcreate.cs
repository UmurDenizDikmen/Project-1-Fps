using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healtcreate : MonoBehaviour
{
    public List<GameObject> Healtpoint = new List<GameObject>();
    public GameObject Healtbox;
    public static bool Createhealt;
    void Start()
    {
        Createhealt= false;
        StartCoroutine(Createammo());
    }
    IEnumerator Createammo()
    {
        while (true)
        {
            yield return null;
            if (Createhealt == false)
            {
                yield return new WaitForSeconds(5f);
                int r�ndex = Random.Range(0, 4);
                Instantiate(Healtbox,Healtpoint[r�ndex].transform.position, Healtpoint[r�ndex].transform.rotation);
                Createhealt = true;
            }
        }
    }
}
