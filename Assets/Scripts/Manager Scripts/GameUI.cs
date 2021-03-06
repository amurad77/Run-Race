using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public GameObject inGame, leaderboard;

    public Text currentLevelText, nextLevelText;

    public Image fill;

    public Sprite oragne, gray;

    private Button nextLevel;
    public Text countText;

    void Awake()
    {
        instance = this;
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if (GameManager.instance.failed)
        {
            if (leaderboard.activeInHierarchy)
            {
                GameManager.instance.failed = false;
                Restart();
            }
        }
    }

    IEnumerator StartGame()
    {
        countText.text = 3.ToString();
        yield return new WaitForSeconds(1);
        countText.color = Color.magenta;
        countText.text = 2.ToString();
        yield return new WaitForSeconds(1);
        countText.color = Color.yellow;
        countText.text = 1.ToString();
        yield return new WaitForSeconds(1);
        countText.color = Color.green;
        countText.text  = "GO";
        GameManager.instance.start = true;
        yield return new WaitForSeconds(.5f);
        countText.gameObject.SetActive(false);

    }

    public void OpenLB()
    {
        inGame.SetActive(false);
        leaderboard.SetActive(true);


        if (GameManager.instance.failed)
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1) + 1 + "";
            fill.sprite = gray;
        }

        else
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1) -1 + "";
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            fill.sprite = oragne;
        }





    }

    private void Restart()
    {
        nextLevel = GameObject.Find("/GameUI/LeaderboardPanel/NextLevel").GetComponent<Button>();
        nextLevel.onClick.RemoveAllListeners();
        nextLevel.onClick.AddListener(() => Reload());
        nextLevel.transform.GetChild(0).GetComponent<Text>().text = "Again";
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
