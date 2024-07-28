using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataSaver : MonoBehaviour
{

    public static SceneDataSaver Instance;
    public int coins;
    public int day;
    public bool dayEnd = false;

    private void Awake()
    {
        if(SceneDataSaver.Instance == null)
        {
            coins = 0;
            day = 0;
            SceneDataSaver.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
    }

}
