using UnityEngine;

public class BottleFiller : MonoBehaviour
{
    ContainerManager containerManager;
    GameObject liquidDrop;
    public int fillAmount;

    // Start is called before the first frame update
    void Awake()
    {
        containerManager = GetComponent<ContainerManager>();
        liquidDrop = transform.Find("Drop").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (containerManager.dropsTotal < fillAmount)
        {
            Instantiate(liquidDrop, transform.position, Quaternion.identity).SetActive(true);
        }
    }
}
