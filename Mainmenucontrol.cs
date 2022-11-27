using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenucontrol : MonoBehaviour
{
    public GameObject Loadingpanel;
    public Slider slider;
    public GameObject Leavepanel;
   public void Gamestart()
   {
     StartCoroutine(Loadinggame());
     Time.timeScale = 1;
   }
    IEnumerator Loadinggame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        Loadingpanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
   public void Exitgame()
   {
     Leavepanel.SetActive(true);
   }
   public void Choose(string Buttoname)
   {
     if(Buttoname == "YES")
     Application.Quit();
     else
     Leavepanel.SetActive(false);
   }
}
