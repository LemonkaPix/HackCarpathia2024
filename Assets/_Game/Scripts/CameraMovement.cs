   using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 borderPos;
    [SerializeField] private float speed = 1f;
    

    private void Update()
    {
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector3 targetPosition = transform.position + (Vector3)(velocity * (speed * Time.deltaTime));

        // Clamp targetPosition within borderPos
        targetPosition.x = Mathf.Clamp(targetPosition.x, -borderPos.x, borderPos.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -borderPos.y, borderPos.y);

        transform.position = targetPosition;
    }
}
