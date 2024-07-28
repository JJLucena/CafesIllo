using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField]
    private GameObject sandwitch;
    public ClientManager clientManager;
    public CoffeeOrderData order;
    public CoinsManager coinsManager;

    private bool OrderTacken = false;
    private bool gone = false;

    private float waitingTime = 5;
    private float waitingTimeAux = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetOrder();
        waitingTime = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingTimeAux > waitingTime && !gone)
        {
            Failed();
        }
        waitingTimeAux += Time.deltaTime;
    }

    public void Failed()
    {
        coinsManager.CoffeeFailed();
        nextClient();
    }

    public void served(CoffeeData coffeeData)
    {
        coinsManager.CoffeeDelivered(coffeeData,order);
        nextClient();
    }

    private void nextClient(){
            gone = true;
            clientManager.nextClient(this.gameObject);
    }

    void OnMouseDown(){
        if(!OrderTacken || SceneDataSaver.Instance.dayEnd){
            if(clientManager.OrderAdded(order, this)){
                OrderTacken = true;
                waitingTimeAux =- waitingTime;
                sandwitch.SetActive(false);
            }
        }
    }

    public void SetOrder()
    {
        OrderTacken = false;
        gone = false;
        waitingTimeAux = 0;
        sandwitch.SetActive(true);

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
