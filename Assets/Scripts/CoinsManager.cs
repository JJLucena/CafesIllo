using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    public TMP_Text textCoins;

    // Start is called before the first frame update
    void Start()
    {
        textCoins.text = SceneDataSaver.Instance.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoffeeFailed()
    {
        SceneDataSaver.Instance.coins -= 20 +  SceneDataSaver.Instance.day * 2;
        textCoins.text = SceneDataSaver.Instance.coins.ToString();
    }

    public void CoffeeDelivered(CoffeeData coffeeData, CoffeeOrderData coffeeOrderData)
    {
        //porcentajes genericos
        float[] generalQuantitiesOrder = new float[3];

        for (int i = 0; i < 3; i++)
        {        
            if(coffeeOrderData.id[i] <= 2)
                generalQuantitiesOrder[0] += coffeeOrderData.percents[i];
            else if(coffeeOrderData.id[i] <= 5)
                generalQuantitiesOrder[1] += coffeeOrderData.percents[i];
            else 
                generalQuantitiesOrder[2] += coffeeOrderData.percents[i];
        }


        for (int i = 0; i < 3; i++)
        {
            if(coffeeData.generalQuantities[i] < generalQuantitiesOrder[i] + 15 && 
            coffeeData.generalQuantities[i] > generalQuantitiesOrder[i] - 15 )
            SceneDataSaver.Instance.coins += 15;
        }

        //porcentajes específicos 
        for (int i = 0; i < 3; i++)
        {       
            if(coffeeOrderData.id[i] <= 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(coffeeData.coffeQuantities[j] > 0 && coffeeOrderData.id[i] == j)            
                        SceneDataSaver.Instance.coins += 15;
                }
            }
            else if(coffeeOrderData.id[i] <= 5)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(coffeeData.milkQuantities[j] > 0 && coffeeOrderData.id[i] == j +3)            
                        SceneDataSaver.Instance.coins += 15;
                }
            }
            else 
            {
                for (int j = 0; j < 3; j++)
                {
                    if(coffeeData.syrupQuantities[j] > 0 && coffeeOrderData.id[i] == j + 6)            
                        SceneDataSaver.Instance.coins += 15;
                }
            }
        }
        
        textCoins.text = SceneDataSaver.Instance.coins.ToString();
    }
}
