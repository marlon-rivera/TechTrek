using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class Generator : MonoBehaviour
{

    private string filePath = "Assets/data/numbers.csv";

    void Start()
    {
        GetDataFromAPI();

    }

 IEnumerator GetDataFromAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:5000/get_number"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al obtener datos desde la API: " + request.error);
            }
            else
            {
                // La solicitud fue exitosa, deserializa los datos JSON
                string responseData = request.downloadHandler.text;
                FloatArrayWrapper wrapper = JsonUtility.FromJson<FloatArrayWrapper>(responseData);

                if (wrapper != null && wrapper.data != null)
                {
                    // Accede a los datos deserializados
                    float[] floatArray = wrapper.data;
                    Debug.Log("Arreglo de flotantes recibido desde la API: " + string.Join(", ", floatArray));
                }
                else
                {
                    Debug.LogError("Error al deserializar los datos JSON");
                }
            }
        }
    }

    public float Next()
    {

        if (File.Exists(filePath))
        {
            string numberGenerated = File.ReadAllText(filePath);
            numberGenerated = numberGenerated.Replace(".", ",");
            File.WriteAllText(filePath, string.Empty);
            Debug.Log(numberGenerated);
            return float.Parse(numberGenerated);
        }
        else
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + filePath);
            return 0.0f;
        }

    }
}
