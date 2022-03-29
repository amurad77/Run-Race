using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ColorSectionManager : MonoBehaviour
{
    private Camera cameraMain;
    private int currentPlayer = 0;

    public float speed = 0.5f;
    public float selectionPos = 13;


    public GameObject charParent;

    public Button playBtn, buyBtn, newBtn, prevBtn;

    public int points = 60;
    public int[] colorPrice;

    void Awake()
    {
        cameraMain = Camera.main;
        CameraPos();
        CheckIfBought();
    }

    public void Buy()
    {
        switch (currentPlayer)
        {
            case 1:
                if(points >= colorPrice[0] && PlayerPrefs.GetInt("BlueBuy", 0) == 0)
                {
                    PlayerPrefs.SetInt("BlueBuy", 1);
                    points -= colorPrice[0];
                }
                break;

            case 2:
                if(points >= colorPrice[1] && PlayerPrefs.GetInt("YellowBuy", 0) == 0)
                {
                    PlayerPrefs.SetInt("YellowBuy", 1);
                    points -= colorPrice[1];
                }
                break;

            case 3:
                if(points >= colorPrice[2] && PlayerPrefs.GetInt("GreenBuy", 0) == 0)
                {
                    PlayerPrefs.SetInt("GreenBuy", 1);
                    points -= colorPrice[2];
                }
                break;
        }
    }




    void CheckIfBought()
    {

        buyBtn.interactable = true;
        playBtn.interactable = true;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Buy";

        buyBtn.image.color = Color.blue;

        switch (currentPlayer)
        {
            case 0:
                buyBtn.interactable = false;
                buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                break;
            
            case 1:
                if(PlayerPrefs.GetInt("BlueBuy") == 1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;   

                    if (points >= colorPrice[0])
                        buyBtn.image.color = Color.green;
                    else
                        buyBtn.image.color = Color.red;
                }
                break;



            
            case 2:
                if(PlayerPrefs.GetInt("YellowBuy") == 1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;   

                    if (points >= colorPrice[0])
                        buyBtn.image.color = Color.green;
                    else
                        buyBtn.image.color = Color.red;
                }
                break;




            case 3:
                if(PlayerPrefs.GetInt("GreenBuy") == 1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;   

                    if (points >= colorPrice[0])
                        buyBtn.image.color = Color.green;
                    else
                        buyBtn.image.color = Color.red;
                }
                break;
        }


    }





    void CameraPos()
    {
        currentPlayer = PlayerPrefs.GetInt("PlayerColor");

        cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + (currentPlayer * 13),
            cameraMain.transform.position.y, cameraMain.transform.position.z);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("PlayerColor", currentPlayer);
        print(PlayerPrefs.GetInt("PlayerColor"));
    }

    public void Next()
    {
        if(currentPlayer < charParent.transform.childCount - 1)
        {
            currentPlayer++;
            StartCoroutine(MoveToNext());
            CheckIfBought();
        }
    }

    public void Prev()
    {
        if(currentPlayer > 0)
        {
            currentPlayer--;
            StartCoroutine(MoveToPrev());
            CheckIfBought();
        }
    }

    IEnumerator MoveToNext()
    {
        Vector3 tempPos = new Vector3(cameraMain.transform.position.x + selectionPos,
            cameraMain.transform.position.y, cameraMain.transform.position.z);
            newBtn.interactable = false;
            prevBtn.interactable = false;
        while(cameraMain.transform.position.x < tempPos.x)
        {
            cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, tempPos, speed);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
        newBtn.interactable = true;
        prevBtn.interactable = true;
        yield return null;
    }


    IEnumerator MoveToPrev()
    {
        Vector3 tempPos = new Vector3(cameraMain.transform.position.x - selectionPos,
            cameraMain.transform.position.y, cameraMain.transform.position.z);
            newBtn.interactable = false;
            prevBtn.interactable = false;
        while(cameraMain.transform.position.x > tempPos.x)
        {
            cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, tempPos, speed);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }


        newBtn.interactable = true;
        prevBtn.interactable = true;
        yield return null;
    }
}
