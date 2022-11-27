using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammobox : MonoBehaviour
{
    public List<Sprite> Gunimage = new List<Sprite>();
    public Image Imageofgun;
    string[] Guns =  
    {
        "Ak",
        "Magnum",
        "Shotgun",
        "Sniper"
    };
    int[] Numbs =
    {
        10,
        20,
        5,
        30
    };
    public string Gunmodel;
    public int Number;
    void Start()
    {
        int Index = Random.Range(0, Guns.Length);
        Imageofgun.sprite = Gunimage[Index];
        Gunmodel = Guns[Index];
        Number = Numbs[Random.Range(0, Numbs.Length)];
    }
}
