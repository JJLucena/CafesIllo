using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeOrder : MonoBehaviour
{
    public CoffeeOrderData data;

    [SerializeField]
    private Transform[] bars;
    [SerializeField]
    private float scaleBars = 0.5f;

    [SerializeField]    
    private Sprite[] sprites; 
    [SerializeField]    
    private Sprite[] idSprites;

    public void SetData()
    {
        for (int i = 0; i < 3; i++)
        {
            bars[i].localScale = new Vector3(data.percents[i]  / 100 * scaleBars, bars[i].localScale.y, bars[i].localScale.z);
        }

        for (int i = 0; i < 3; i++)
        {
            //idSprites[i] = ;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
