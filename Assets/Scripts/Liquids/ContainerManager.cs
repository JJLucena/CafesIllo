using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    private Collider2D containerArea;
    private List<Collider2D> containedDrops = new();
    public float dropsTotal;
    private CoffeeData containedBalls = new();
    public CoffeeData containedPercentages = new();

    // Start is called before the first frame update
    void Start()
    {
        containerArea = transform.Find("ContainerArea").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        containerArea.OverlapCollider(new ContactFilter2D().NoFilter(), containedDrops);
        containedBalls = new();

        // Count how many drops of each category are in the container
        foreach (Collider2D drop in containedDrops)
        {
            switch (LayerMask.LayerToName(drop.gameObject.layer)) {
                case "Coffee White":
                    containedBalls.coffeQuantities[0]++;
                    containedBalls.generalQuantities[0]++;
                    break;
                case "Coffee Brown":
                    containedBalls.coffeQuantities[1]++;
                    containedBalls.generalQuantities[0]++;
                    break;
                case "Coffee Black":
                    containedBalls.coffeQuantities[2]++;
                    containedBalls.generalQuantities[0]++;
                    break;
                case "Milk Animal":
                    containedBalls.milkQuantities[0]++;
                    containedBalls.generalQuantities[1]++;
                    break;
                case "Milk Soy":
                    containedBalls.milkQuantities[1]++;
                    containedBalls.generalQuantities[1]++;
                    break;
                case "Milk Almond":
                    containedBalls.milkQuantities[2]++;
                    containedBalls.generalQuantities[1]++;
                    break;
                case "Syrup Straw":
                    containedBalls.syrupQuantities[0]++;
                    containedBalls.generalQuantities[2]++;
                    break;
                case "Syrup Honey":
                    containedBalls.syrupQuantities[1]++;
                    containedBalls.generalQuantities[2]++;
                    break;
                case "Syrup Blue":
                    containedBalls.syrupQuantities[2]++;
                    containedBalls.generalQuantities[2]++;
                    break;
            }
        }

        // Calculate percentages based on the drops
        containedPercentages.generalQuantities = CalculatePercentages(containedBalls.generalQuantities);
        containedPercentages.coffeQuantities = CalculatePercentages(containedBalls.coffeQuantities);
        containedPercentages.milkQuantities = CalculatePercentages(containedBalls.milkQuantities);
        containedPercentages.syrupQuantities = CalculatePercentages(containedBalls.syrupQuantities);
    }

    float[] CalculatePercentages(float[] dropAmounts) {

        float[] percentages = new float[dropAmounts.Length];
        float total = 0;

        foreach (float amount in dropAmounts)
        {
            total += amount;
        }

        for (int i = 0; i < dropAmounts.Length; i++)
        {
            percentages[i] = dropAmounts[i] / total * 100;
        }

        dropsTotal = total;
        return percentages;
    }
}
