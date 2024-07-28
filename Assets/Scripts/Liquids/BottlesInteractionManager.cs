using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesInteractionManager : MonoBehaviour
{
    private bool holdingBottle = false;
    private bool rotatingBottle = false;
    private GameObject instancedBottle;
    private Vector3 rotatingStartPosition = new();

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;

        if (holdingBottle)
        {
            Rigidbody2D rb = instancedBottle.gameObject.GetComponent<Rigidbody2D>();

            if (Input.GetMouseButton(1))
            {
                if (!rotatingBottle)
                {
                    rb.velocity = Vector2.zero;
                    rotatingStartPosition = mousePosition;
                    rotatingBottle = true;
                }
            }
            if (rotatingBottle)
            {
                float targetAngle = (mousePosition.x - rotatingStartPosition.x) * 10 + 180;
                float torque = 10f;

                // Calculate the difference between the current angle and the target angle
                float currentAngle = rb.rotation;
                float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

                // Apply damping for smoother rotation
                rb.angularVelocity = angleDifference * torque;
            } else {
                float speed = 50f;
                float stopDistance = 0.1f;

                // Calculate the direction vector from the current position to the mouse position
                Vector2 direction = (mousePosition - (Vector3)rb.position).normalized;

                // Calculate the distance to the mouse position
                float distance = Vector2.Distance(mousePosition, rb.position);

                if (distance > stopDistance)
                {
                    rb.velocity = direction * speed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        } else {
            foreach (Transform child in transform)
            {
                if (Input.GetMouseButton(0) && child.GetComponent<Collider2D>().OverlapPoint(mousePosition))
                {
                    holdingBottle = true;
                    instancedBottle = Instantiate(child.Find("Bottle").gameObject, mousePosition, Quaternion.Euler(0, 0, 180));
                    instancedBottle.SetActive(true);
                }
            }
        }

        if (!Input.GetMouseButton(1) && rotatingBottle)
        {
            rotatingBottle = false;
        }

        if (!Input.GetMouseButton(0) && instancedBottle)
        {
            holdingBottle = false;
            foreach (Collider2D drop in instancedBottle.GetComponent<ContainerManager>().containedDrops)
            {
                Destroy(drop.gameObject);
            }
            Destroy(instancedBottle);
        }
    }
}
