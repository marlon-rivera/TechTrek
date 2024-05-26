using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPopUps : MonoBehaviour
{
    // Definir las claves de PlayerPrefs como constantes
    private const string FestivoKey = "festivo";
    private const string ParoKey = "paro";
    private const string TutoriaKey = "tutoria";
    private const string PresentacionKey = "presentacion";
    private const string MalexKey = "malex";
    private const string DatusKey = "datus";
    private const string CyberKey = "cyber";
    private const string ErrorusKey = "errorus";

    // Variables públicas para el estado de los pop-ups
    public static bool festivo;
    public static bool paro;
    public static bool tutoria;
    public static bool presentacion;
    public static bool malex;
    public static bool datus;
    public static bool cyber;
    public static bool errorus;

    // Inicializa las variables al inicio
    void Initialize()
    {
        LoadData();
    }

    // Método para cargar los datos desde PlayerPrefs
    public static void LoadData()
    {
        festivo = PlayerPrefs.GetInt(FestivoKey) == 1;
        paro = PlayerPrefs.GetInt(ParoKey) == 1;
        tutoria = PlayerPrefs.GetInt(TutoriaKey) == 1;
        presentacion = PlayerPrefs.GetInt(PresentacionKey) == 1;
        malex = PlayerPrefs.GetInt(MalexKey) == 1;
        datus = PlayerPrefs.GetInt(DatusKey) == 1;
        cyber = PlayerPrefs.GetInt(CyberKey) == 1;
        errorus = PlayerPrefs.GetInt(ErrorusKey, 0) == 1;
    }

    // Método para guardar los datos en PlayerPrefs
    public static void SaveData()
    {
        PlayerPrefs.SetInt(FestivoKey, festivo ? 1 : 0);
        PlayerPrefs.SetInt(ParoKey, paro ? 1 : 0);
        PlayerPrefs.SetInt(TutoriaKey, tutoria ? 1 : 0);
        PlayerPrefs.SetInt(PresentacionKey, presentacion ? 1 : 0);
        PlayerPrefs.SetInt(MalexKey, malex ? 1 : 0);
        PlayerPrefs.SetInt(DatusKey, datus ? 1 : 0);
        PlayerPrefs.SetInt(CyberKey, cyber ? 1 : 0);
        PlayerPrefs.SetInt(ErrorusKey, errorus ? 1 : 0);
    }
}
