using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public GameObject clientManager;
    public GameObject orderManager;
    public GameObject endDayPanel;
    public GameObject finalDayPanel;
    public GameObject GameOverPanel;
    public TMP_Text day;
    [SerializeField]
    private int dayTime = 10;
    [SerializeField]
    private int dayCost = 100;
    private float dayTimeAux = 0;

    // Start is called before the first frame update
    void Start()
    {
        day.text = SceneDataSaver.Instance.day.ToString();
        endDayPanel.SetActive(false);
        finalDayPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
{
        if(dayTimeAux >= dayTime)
        {

            if(dayCost + SceneDataSaver.Instance.day * 20 > SceneDataSaver.Instance.coins ){
                GameOverPanel.SetActive(true);
                GameOverPanel.transform.GetChild(0).GetComponent<TextMeshPro>();
                clientManager.SetActive(false);
                orderManager.SetActive(false);
                Invoke("GameOver",2f);
            }
            else if(SceneDataSaver.Instance.day >= 5)
            {
                finalDayPanel.SetActive(true);
                clientManager.SetActive(false);
                orderManager.SetActive(false);
                Invoke("Final",2f);
            }
            else{
                endDayPanel.SetActive(true);
                clientManager.SetActive(false);
                orderManager.SetActive(false);
                Invoke("ResetScene",2f);
            }

        }else
            dayTimeAux += Time.deltaTime;
    }

    void ResetScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void Final()
    {
        SceneDataSaver.Instance.day = 0;
        SceneDataSaver.Instance.coins = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
