using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDrop : MonoBehaviour
{
    public Client client;
    public CoffeeData coffeeData = new CoffeeData();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(client != null)
        client.served(coffeeData);
    }
}
