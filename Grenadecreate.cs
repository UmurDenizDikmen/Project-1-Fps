using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadecreate : MonoBehaviour
{
    public List<GameObject> Grenadepoint = new List<GameObject>();
    public GameObject Grenade;
    public static bool Creategrenade;
    void Start()
    {
        Creategrenade = false;
        StartCoroutine(Createammo());
    }
    IEnumerator Createammo()
    {
        while (true)
        {
            yield return null;
            if (Creategrenade == false)
            {
                yield return new WaitForSeconds(5f);
                int r�ndex = Random.Range(0, 4);
                Instantiate(Grenade, Grenadepoint[r�ndex].transform.position, Grenadepoint[r�ndex].transform.rotation);
                Creategrenade = true;
            }
        }
    }
}
