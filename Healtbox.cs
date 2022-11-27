using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healtbox : MonoBehaviour
{
    int[] Healts =
    {
        2,
        4,
        6,
        8,
    };
    public int Healtnumb;
    void Start()
    {
        int Index = Random.Range(0, 4);
        Healtnumb = Healts[Index];
    }
}
