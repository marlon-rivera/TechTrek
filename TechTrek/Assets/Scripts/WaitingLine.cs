using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : MonoBehaviour
{
    private static int[] arrivalTimeProb = new int[] { 0, 25, 50, 75 };
    private static int[] serviceTimeProb = new int[] { 0, 20, 60, 85 };
    public static int[] arrivalTimeSum = new int[4];
    public static int[] serviceTimeSum = new int[4];

    public static int[] CalculateArrivalTimes()
    {
        int[] arrivalTime = new int[4];
        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * FindObjectOfType<Generator>().GetNextNumber());

            if (number <= arrivalTimeProb[1])
            {
                arrivalTime[i] = 16;
            }
            else if (number <= arrivalTimeProb[2])
            {
                arrivalTime[i] = 17;

            }
            else if (number <= arrivalTimeProb[3])
            {
                arrivalTime[i] = 18;
            }
            else
            {
                arrivalTime[i] = 19;
            }
            if (i == 0)
            {
                arrivalTimeSum[i] = arrivalTime[i];
            }
            else
            {
                arrivalTimeSum[i] = arrivalTimeSum[i - 1] + arrivalTime[i];
            }
        }
        return arrivalTime;
    }

    public static int[] CalculateServicesTime()
    {
        int[] serviceTime = new int[4];
        for (int i = 0; i < 4; i++)
        {
            int number = (int)(0 + (99 - 0) * FindObjectOfType<Generator>().GetNextNumber());

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
        return serviceTime;
    }
}
