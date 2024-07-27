using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    private  OrderManager orderManager;

    [SerializeField]
    private GameObject client;

    [SerializeField]
    private int numClients;    

    [SerializeField]
    private Transform[] clientsPositions;
    private Queue<GameObject> clientsActivated;
    private Queue<GameObject> clientsDeactivated;
    [SerializeField]
    private float timer;
    private float timerAux;

    // Start is called before the first frame update
    void Start()
    {
        clientsActivated = new Queue<GameObject>();
        clientsDeactivated = new Queue<GameObject>();


        for (int i = 0; i < numClients; i++)
        {            
            GameObject createdClient = Instantiate(client, gameObject.transform.position, Quaternion.identity);
            createdClient.GetComponent<Client>().clientManager = this;
            createdClient.SetActive(false);
            clientsDeactivated.Enqueue(createdClient);
        }
        for (int i = 0; i < clientsPositions.Length; i++)
        {  
            clientsDeactivated.Peek().transform.position = clientsPositions[i].position;
            SetNextActivate();
        }
    }

    public void SetNextActivate()
    {
        clientsDeactivated.Peek().SetActive(true);
        clientsDeactivated.Peek().GetComponent<Client>().SetOrder();
        clientsActivated.Enqueue(clientsDeactivated.Dequeue());
    }

    public void SetLastDeactivate()
    {
        clientsActivated.Peek().SetActive(false);
        orderManager.RemoveOrder(clientsActivated.Peek().GetComponent<Client>().order);
        clientsDeactivated.Enqueue(clientsActivated.Dequeue());
    }

    public bool OrderAdded(CoffeeOrderData newOrder)
    {
        if(orderManager != null){
            if(orderManager.OrderAdded(newOrder))
                return true;
        }
        return false;
    }

    void Update()
    {
        if(timerAux >= timer)
        {
            clientsDeactivated.Peek().transform.position = clientsActivated.Peek().transform.position;
            SetNextActivate();
            SetLastDeactivate();
            timerAux = 0;
        }
        timerAux += Time.deltaTime;
    }

}
