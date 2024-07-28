using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMovement : MonoBehaviour
{
    private Collider2D myCollider;
    private bool calculatedOffset;
    private Vector3 cursorOffset;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;

        // Check if the left mouse button is being held down and the mouse is within the Collider2D
        if (Input.GetMouseButton(0) && myCollider.OverlapPoint(mousePosition))
        {
            if (!calculatedOffset)
            {
                cursorOffset = transform.parent.position - mousePosition;
                calculatedOffset = true;
            }
            Cursor.visible = false;
            transform.parent.position = mousePosition + cursorOffset;
        } else {
            Cursor.visible = true;
            calculatedOffset = false;
        }
    }
}
