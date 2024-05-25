using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
   public static void StopGame(){
    Time.timeScale = 0f;
   }

   public static void ResumeGame(){
    Time.timeScale = 1f;
   }

}
