using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteCarlo : MonoBehaviour
{
    private static float[] luckOne = new float[] { 0.26f, 0.526f, 0.8f, 0.9f, 1.0f };
    private static float[] luckTwo = new float[] { 0.1f, 0.37f, 0.64f, 0.91f, 1.0f };
    private static float[] luckThree = new float[] { 0.1f, 0.2f, 0.47f, 0.74f, 1.0f };

    public static GameObject getPrefab(int lucky)
    {
        float randomNumber = FindObjectOfType<Generator>().GetNextNumber();
        if (lucky == 1)
        {
            if (randomNumber <= luckOne[0])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckOne[1])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckOne[2])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckOne[3])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckOne[4])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
        else if (lucky == 2)
        {
            if (randomNumber <= luckTwo[0])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckTwo[1])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckTwo[2])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckTwo[3])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckTwo[4])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
        else if (lucky == 3)
        {
            if (randomNumber <= luckThree[0])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota1");
            }
            else if (randomNumber <= luckThree[1])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota2");
            }
            else if (randomNumber <= luckThree[2])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota3");
            }
            else if (randomNumber <= luckThree[3])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota4");
            }
            else if (randomNumber <= luckThree[4])
            {
                return Resources.Load<GameObject>("prefabs/notas/Nota5");
            }
        }
        return null;
    }
}
