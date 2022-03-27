using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject[] runners;
    private bool TempFalse;


    List<RakingSystem> sortArray = new List<RakingSystem>();

    public int pass;

    public string firstPlace, secondPlace, thirdPlace;
    

    void Awake()
    {
        instance = this;
        runners = GameObject.FindGameObjectsWithTag("Runner");
    }


    void Start()
    {
        for (int i = 0; i < runners.Length; i++)
        {
            sortArray.Add(runners[i].GetComponent<RakingSystem>());
            
        }
    }

    void Update()
    {
        CalculatingRank();
    }

    void CalculatingRank()
    {
        sortArray = sortArray.OrderBy(t => t.counter).ToList();
        switch (sortArray.Count)
        {
            case 3:
                sortArray[0].rank = 3;
                sortArray[1].rank = 2;
                sortArray[2].rank = 1;
                break;

            case 2:
                sortArray[0].rank = 2;
                sortArray[1].rank = 1;
                break;

            case 1:
                sortArray[0].rank = 1;
                if (firstPlace == "")
                {
                    firstPlace = sortArray[0].name;
                    //Open
                }
                break;
        }



        if(pass >= (float)runners.Length / 2)
        {
            pass = 0;
            sortArray = sortArray.OrderBy(t => t.counter).ToList();
            foreach (RakingSystem rs in sortArray)
            {
                if(rs.rank == sortArray.Count)
                {
                    if(rs.gameObject.name == "Player")
                    {
                        //werwe
                        //werwer
                    }

                    if (thirdPlace == "")
                        thirdPlace = rs.gameObject.name;
                    else if (secondPlace == "")
                        secondPlace = rs.gameObject.name;

                    rs.gameObject.SetActive(false);
                }
            }

            runners = GameObject.FindGameObjectsWithTag("Runner");

            sortArray.Clear();
            for (int i = 0; i < runners.Length; i++)
            {
                sortArray.Add(runners[i].GetComponent<RakingSystem>());
            }
        }
    }
}
