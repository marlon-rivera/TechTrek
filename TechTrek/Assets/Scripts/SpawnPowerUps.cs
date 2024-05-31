using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnPowerUps : MonoBehaviour
{

    private int[] arrivalTime = new int[4];
    private int[] serviceTime = new int[4];
    private float minX, maxX, minY, maxY;
    public Transform[] points = new Transform[4];
    public GameObject[] powerups;
    private int[] arrivalTimeSum = new int[4];
    private int[] serviceTimeSum = new int[4];
    private int time = 0;
    private GameObject powerup = null;
    private int posArrival = 0;
    private int posService = 0;
    public Camera mainCamera;
    private string[] types = new string[] { "festivo", "paro", "tutoria" };
    private string actualType;
    public GameObject[] typesAssigned;
    private int powerRandom;

    void Start()
    {
        FindObjectOfType<Generator>().LoadData();
        mainCamera = Camera.main;
 
        arrivalTime = WaitingLine.CalculateArrivalTimes();
        serviceTime = WaitingLine.CalculateServicesTime();
        arrivalTimeSum = WaitingLine.arrivalTimeSum;
        serviceTimeSum = WaitingLine.serviceTimeSum;        

        typesAssigned[0].SetActive(false);
        typesAssigned[1].SetActive(false);
        typesAssigned[2].SetActive(false);

        InvokeRepeating("IncreaseTime", 1f, 1f);
    }




    void IncreaseTime()
    {
        time++;


        if (posArrival < 4)
        {
            if (time == arrivalTimeSum[posArrival]) 
            {
                Vector2 spawnPosition = new Vector2((int)(minX + (maxX - minX) * FindObjectOfType<Generator>().GetNextNumber() + 100), (int)(minY + (maxY - minY) * FindObjectOfType<Generator>().GetNextNumber()));
                powerRandom = (int)(0 + (powerups.Length - 0) * FindObjectOfType<Generator>().GetNextNumber());
                actualType = types[powerRandom];
                
                powerup = Instantiate(powerups[powerRandom], spawnPosition, Quaternion.identity);
                posArrival++;
            }
        }
        if (posService < 4)
        {
            if (time == serviceTimeSum[posService])
            {
                Destroy(powerup);
                Player player = FindObjectOfType<Player>();
                player.RemovePowerUp(actualType);
                typesAssigned[powerRandom].SetActive(false);
                posService++;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        points[0].position = bottomLeft;
        points[1].position = topLeft;
        points[2].position = topRight;
        points[3].position = bottomRight;

        maxX = points.Max(point => point.position.x);
        minX = points.Min(point => point.position.x);
        minY = points.Min(point => point.position.y);
        maxY = points.Max(point => point.position.y);
    }

    public void ShowPowerUp()
    {
        typesAssigned[powerRandom].SetActive(true);
    }

    public string GetPowerUpType()
    {
        return actualType;
    }
}

