using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPowerUps : MonoBehaviour
{
    private int[] arrivalTimeProb = new int[] { 0, 25, 50, 75 };
    private int[] serviceTimeProb = new int[] { 0, 20, 60, 85 };
    private int[] arrivalTime = new int[4];
    private int[] serviceTime = new int[4];
    private float minX, maxX, minY, maxY;
    public Transform[] points;
    public GameObject[] powerups;
    private int[] arrivalTimeSum = new int[4];
    private int[] serviceTimeSum = new int[4];
    private int time = 0;
    private GameObject powerup = null;

    void Start()
    {
        Generator.LoadData();
        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * Generator.GetNextNumber());

            if (number <= arrivalTimeProb[1])
            {
                arrivalTime[i] = 16;
                if (i == 0)
                {
                    arrivalTimeSum[i] = arrivalTime[i];
                }
                else
                {
                    arrivalTimeSum[i] = arrivalTime[i - 1] + arrivalTime[i];
                }
            }
            else if (number <= arrivalTimeProb[2])
            {
                arrivalTime[i] = 17;
                if (i == 0)
                {
                    arrivalTimeSum[i] = arrivalTime[i];
                }
                else
                {
                    arrivalTimeSum[i] = arrivalTime[i - 1] + arrivalTime[i];
                }
            }
            else if (number <= arrivalTimeProb[3])
            {
                arrivalTime[i] = 18;
                if (i == 0)
                {
                    arrivalTimeSum[i] = arrivalTime[i];
                }
                else
                {
                    arrivalTimeSum[i] = arrivalTime[i - 1] + arrivalTime[i];
                }
            }
            else
            {
                arrivalTime[i] = 19;
                if (i == 0)
                {
                    arrivalTimeSum[i] = arrivalTime[i];
                }
                else
                {
                    arrivalTimeSum[i] = arrivalTime[i - 1] + arrivalTime[i];
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * Generator.GetNextNumber());

            if (number <= serviceTimeProb[1])
            {
                serviceTime[i] = 4;


            }
            else if (number <= serviceTimeProb[2])
            {
                serviceTime[i] = 5;
            }
            else if (number <= serviceTimeProb[3])
            {
                serviceTime[i] = 6;
            }
            else
            {
                serviceTime[i] = 7;
            }
            serviceTimeSum[i] = arrivalTimeSum[i] + serviceTime[i];
        }
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Tiempo: " + (i + 1) + ", arrival: " + arrivalTime[i] + ", service: " + serviceTime[i]);
        }
        maxX = points.Max(point => point.position.x);
        minX = points.Min(point => point.position.x);
        minY = points.Min(point => point.position.y);
        maxY = points.Max(point => point.position.y);
        Debug.Log(arrivalTime);
        InvokeRepeating("IncreaseTime", 1f, 1f);
    }




    void IncreaseTime()
    {
        time++;
        int posArrival = 0;
        int posService = 0;

        if (time == arrivalTimeSum[posArrival])
        {
            Vector2 spawnPosition = new Vector2((int)(minX + (maxX - minX) * Generator.GetNextNumber()), (int)(minY + (maxY - minY) * Generator.GetNextNumber()));
            int powerRandom = (int)(0 + (powerups.Length - 0) * Generator.GetNextNumber());
            powerup = Instantiate(powerups[powerRandom], spawnPosition, Quaternion.identity);
            posArrival++;
        }
        if (time == serviceTimeSum[posService])
        {
            Destroy(powerup);
            posService++;

        }
    }
}

