using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private CoinsManager coinsManager;

    [SerializeField]
    private GameObject order;
    [SerializeField]
    private GameObject glassDrop;

    [SerializeField]
    private Transform[] transformOrders;
    private GameObject[] orders;
    private GameObject[] glassDrops;

    void Start()
    {
        orders = new GameObject[transformOrders.Length];

        for (int i = 0; i < transformOrders.Length; i++)
        {
            float orderOffset = -2;
            float glassDropOffset = -4;

            GameObject orderObject = Instantiate(order, new Vector3(transformOrders[i].position.x ,transformOrders[i].position.y+orderOffset,transformOrders[i].position.z), Quaternion.identity);
            orderObject.SetActive(false);
            orders[i] = orderObject;
            GameObject glassDropObject = Instantiate(glassDrop, new Vector3(transformOrders[i].position.x,transformOrders[i].position.y+glassDropOffset,transformOrders[i].position.z), Quaternion.identity);
        }
    }

    public bool OrderAdded(CoffeeOrderData newOrder)
    {
        for (int i = 0; i < orders.Length; i++)
        {
            if(orders[i].GetComponent<CoffeeOrder>().data == null)
            {
                orders[i].GetComponent<CoffeeOrder>().data = newOrder;
                orders[i].GetComponent<CoffeeOrder>().SetData();
                orders[i].SetActive(true);
                return true;
            }
        }
        return false;
    }

    public bool RemoveOrder(CoffeeOrderData newOrder)
    {
        for (int i = 0; i < orders.Length; i++)
        {
            if(orders[i].GetComponent<CoffeeOrder>().data == newOrder)
            {
                orders[i].GetComponent<CoffeeOrder>().data = null;
                orders[i].SetActive(false);
                return true;
            }
        }
        return false;
    }
}
