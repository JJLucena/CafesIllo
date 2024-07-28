using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMovement : MonoBehaviour
{
    private Collider2D myCollider;
    private bool holdingGlass;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;
        Rigidbody2D rb = transform.gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetMouseButton(0) && myCollider.OverlapPoint(mousePosition) && !holdingGlass)
        {
            holdingGlass = true;
        }
        if (holdingGlass)
        {
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
        } else {
            Cursor.visible = true;
        }

        if (!Input.GetMouseButton(0) && holdingGlass)
        {
            holdingGlass = false;
        }
    }
}
