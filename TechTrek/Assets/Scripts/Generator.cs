using System.Collections;
using System.Collections.Generic;
using System.IO; // Agregar esta línea para usar la clase File
using UnityEngine;

public class Generator : MonoBehaviour
{

    private string filePath = @"C:\Users\Alejandro\Documents\GitHub\TechTrek\TechTrek\numbers.csv";
    public string numberGenerated;

    void Start()
    {
        if(File.Exists(filePath)){
            numberGenerated = File.ReadAllText(filePath);
            Debug.Log("Numero generado: " + numberGenerated);
            File.WriteAllText(filePath, string.Empty);
            Debug.Log("Contenido del archivo borrado exitosamente.");
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePath);

        }
    }

    void Next(){
        Start();
    }

    // Update is called once per frame
    void Update()
    {
        Next();
    }
}
