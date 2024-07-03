using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExitToDesktop : MonoBehaviour
{
    public void ExitApplication()
    {

        if(UnityEditor.EditorApplication.isPlaying = false) { Debug.Log("Application not running"); }
          else Application.Quit();
    }
}
