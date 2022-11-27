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
                int rýndex = Random.Range(0, 4);
                Instantiate(AmmoBox,Ammopoint[rýndex].transform.position, Ammopoint[rýndex].transform.rotation);
                Create = true;
            }
        }
    }
}
