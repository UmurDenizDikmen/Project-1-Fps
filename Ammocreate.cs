using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammocreate : MonoBehaviour
{
    public List<GameObject> Ammopoint = new List<GameObject>();
    public GameObject AmmoBox;
    public static bool Create;
    void Start()
    {
        Create = false;
        StartCoroutine(Createammo());
    }
    IEnumerator Createammo()
    {
        while (true)
        {
            yield return null;
            if (Create == false)
            {
                yield return new WaitForSeconds(5f);
                int r�ndex = Random.Range(0, 4);
                Instantiate(AmmoBox,Ammopoint[r�ndex].transform.position, Ammopoint[r�ndex].transform.rotation);
                Create = true;
            }
        }
    }
}
