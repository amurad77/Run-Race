using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakingSystem : MonoBehaviour
{

    public int currentCheckPoint = 1, lapCount;

    public float distance;
    private Vector3 checkPoint;

    public float counter;
    public int rank;

    void Start()
    {
        currentCheckPoint = 1;
        checkPoint = GameObject.Find("CheckPoint" + currentCheckPoint).transform.position;
    }

    void Update()
    {
        CalculateDistance();
    }

    void  CalculateDistance()
    {
        distance = Vector3.Distance(transform.position, checkPoint);
        counter = lapCount * 1000 + currentCheckPoint * 100 + distance;
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "CheckPoint")
        {
            currentCheckPoint = target.GetComponent<CurrentCheckPoint>().currentCheckNumber;
            checkPoint = GameObject.Find("CheckPoint" + currentCheckPoint).transform.position;
        }

        if(target.tag == "Finsh")
        {
            lapCount = 1;
            GameManager.instance.pass += 1;
        }
    }
}
