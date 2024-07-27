using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Client : MonoBehaviour
{
    public ClientManager clientManager;
    public CoffeeOrderData order;

    private bool OrderTacken = false;

    // Start is called before the first frame update
    void Start()
    {
        SetOrder();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown(){
        if(!OrderTacken)
            OrderTacken = clientManager.OrderAdded(order);
    }

    public void SetOrder()
    {
        OrderTacken = false;

        float coffeeAux = Random.Range(2, 8);
        float milkAux = Random.Range(2, 8);
        float sweetAux = Random.Range(2, 8);

        float AuxAmount = coffeeAux + milkAux + sweetAux;

        order.percents[0] =( coffeeAux / AuxAmount )* 100;
        order.percents[1] =( milkAux / AuxAmount )* 100;
        order.percents[2] =( sweetAux / AuxAmount )* 100;

        for (int i = 0; i < 3; i++)
        {
            order.id[i] = Random.Range(0, 8);
        }
    }

}
