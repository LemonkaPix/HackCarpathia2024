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
        Vector2 velocity = new Vector2();
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");
        if (velocity != Vector2.zero)
        {
            transform.position += (Vector3)(velocity * (Time.deltaTime * speed));
        }
    }
}
