using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Generator : MonoBehaviour
{
    public Queue<float> numbers = new Queue<float>();
    private string filePath = "Assets/data/numbers.csv";

    void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        numbers = ReadCsvToFloatQueue();
    }

    public float GetNextNumber()
    {
        if (numbers != null && numbers.Count == 50)
        {
            Debug.Log("Quedan 50 numeros, voy a cargar mas");
            StartCoroutine(GetData());


        }
        if (numbers == null || numbers.Count == 0)
        {
            throw new InvalidOperationException("Cargar datos");
        }
        return numbers.Dequeue();
    }

    private Queue<float> ReadCsvToFloatQueue()
    {
        Queue<float> result = new Queue<float>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                foreach (string value in values)
                {
                    string numberStr = value.Replace(".", ",");
                    if (float.TryParse(numberStr, out float number))
                    {
                        result.Enqueue(number);
                    }
                    else
                    {
                        Debug.LogWarning("Failed to parse value as float: " + numberStr);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data from CSV: " + e.Message);
        }
        return result;
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:5000/generate_numbers"))
        {

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al realizar la solicitud: " + request.error);
            }
            else
            {
                Debug.Log("Respuesta de la API: " + request.downloadHandler.text);
                LoadData();
            }
        }
    }
}
