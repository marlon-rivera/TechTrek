using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void GoMap()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void GoToLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
}
