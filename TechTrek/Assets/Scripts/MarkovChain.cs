using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkovChain : MonoBehaviour
{
    private static string[] states = { "taller", "parcial", "expo", "proyecto" };
    private static Dictionary<string, float[]> transitions = new Dictionary<string, float[]>
        {
            { "taller", new float[] { 0.1f, 0.5f, 0.2f, 0.2f } },
            { "parcial", new float[] { 0.3f, 0.1f, 0.4f, 0.2f } },
            { "expo", new float[] { 0.2f, 0.3f, 0.1f, 0.4f } },
            { "proyecto", new float[] { 0.4f, 0.2f, 0.3f, 0.1f } }
        };

    public static string GetNextState(string currentState)
    {
        float[] probs = transitions[currentState];
        float randomValue = FindObjectOfType<Generator>().GetNextNumber();
        float sum = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            sum += probs[i];
            if (randomValue <= sum)
            {
                
                return states[i]; ;
            }
        }
        return null;
    }


}
