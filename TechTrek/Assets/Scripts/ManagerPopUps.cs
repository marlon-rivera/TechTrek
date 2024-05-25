using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPopUps : MonoBehaviour
{

    public bool festivo { get; set; }
    public bool paro { get; set; }
    public bool tutoria { get; set; }
    public bool presentacion { get; set; }
    public bool malex { get; set; }
    public bool datus { get; set; }
    public bool cyber { get; set; }
    public bool errorus { get; set; }

    void Start()
    {
        festivo = PlayerPrefs.GetInt("festivo", 0) == 0 ? false : true;
        paro = PlayerPrefs.GetInt("paro", 0) == 0 ? false : true;
        tutoria = PlayerPrefs.GetInt("tutoria", 0) == 0 ? false : true;
        presentacion = PlayerPrefs.GetInt("presentacion", 0) == 0 ? false : true;
        malex = PlayerPrefs.GetInt("malex", 0) == 0 ? false : true;
        datus = PlayerPrefs.GetInt("datus", 0) == 0 ? false : true;
        cyber = PlayerPrefs.GetInt("cyber", 0) == 0 ? false : true;
        errorus = PlayerPrefs.GetInt("errorus", 0) == 0 ? false : true;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("festivo", festivo ? 1 : 0);
        PlayerPrefs.SetInt("paro", paro ? 1 : 0);
        PlayerPrefs.SetInt("tutoria", tutoria ? 1 : 0);
        PlayerPrefs.SetInt("presentacion", presentacion ? 1 : 0);
        PlayerPrefs.SetInt("malex", malex ? 1 : 0);
        PlayerPrefs.SetInt("datus", datus ? 1 : 0);
        PlayerPrefs.SetInt("cyber", cyber ? 1 : 0);
        PlayerPrefs.SetInt("errorus", errorus ? 1 : 0);
    }

}
