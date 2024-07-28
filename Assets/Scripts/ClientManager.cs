using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField]
    private CoinsManager coinsManager;
    [SerializeField]
    private  OrderManager orderManager;

    [SerializeField]
    private GameObject client;

    [SerializeField]
    private int numClients;    

    [SerializeField]
    private Transform[] clientsPositions;
    private List<GameObject> clientsActivated;
    private List<GameObject> clientsDeactivated;
    [SerializeField]
    private float timer;
    private float timerAux;

    // Start is called before the first frame update
    void Start()
    {
        clientsActivated = new List<GameObject>();
        clientsDeactivated = new List<GameObject>();


        for (int i = 0; i < numClients; i++)
        {            
            GameObject createdClient = Instantiate(client, gameObject.transform.position, Quaternion.identity);
            createdClient.GetComponent<Client>().clientManager = this;
            createdClient.GetComponent<Client>().coinsManager = coinsManager;
            createdClient.SetActive(false);
            clientsDeactivated.Add(createdClient);
        }
        for (int i = 0; i < clientsPositions.Length; i++)
        {  
            clientsDeactivated.First().transform.position = clientsPositions[i].position;
            SetNextActivate();
        }
    }

    public void nextClient(GameObject pastClient)
    {
        if(clientsActivated.Contains(pastClient))
        {
            clientsDeactivated.First().transform.position = pastClient.transform.position;
            SetNextActivate();
            SetLastDeactivate(pastClient);
        }
    }

    public void SetNextActivate()
    {
        clientsDeactivated.First().SetActive(true);
        clientsDeactivated.First().GetComponent<Client>().SetOrder();
        clientsActivated.Add(clientsDeactivated.First());
        clientsDeactivated.Remove(clientsDeactivated.First());
    }

    public void SetLastDeactivate(GameObject pastClient)
    {
        //clientsActivated.Find(x => x == clientsActivated.First());
        pastClient.SetActive(false);
        orderManager.RemoveOrder(pastClient.GetComponent<Client>().order);
        clientsDeactivated.Add(pastClient);
        clientsActivated.Remove(pastClient);
    }

    public bool OrderAdded(CoffeeOrderData newOrder, Client client)
    {
        if(orderManager != null){
            if(orderManager.OrderAdded(newOrder, client))
                return true;
        }
        return false;
    }

    void Update()
    {
        if(timerAux >= timer)
        {
            //nextClient();
            timerAux = 0;
        }
        timerAux += Time.deltaTime;
    }

}
