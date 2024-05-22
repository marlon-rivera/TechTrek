using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public  static List<float> numbers;
    private static int currentIndex = -1; 

    void Start(){
        LoadData();
    }

    public static void LoadData()
    {
        string filePath = "Assets/data/numbers.csv";
        numbers = ReadCsvToFloatList(filePath);
    }
    public static float GetNextNumber()
    {
        if (numbers == null || numbers.Count == 0)
        {
            throw new InvalidOperationException("No data loaded. Call LoadData() first.");
        }

        currentIndex = (currentIndex + 1) % numbers.Count;
        return numbers[currentIndex];
    }

    private static List<float> ReadCsvToFloatList(string filePath)
    {
        List<float> result = new List<float>();

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
                        result.Add(number);
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
        Debug.Log("Numeros cargados: " + result[0]);
        return result;
    }
}
