using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform Position;
    public float maxX = 0.2f;
    public float maxY = 0.1f;

    private void Start()
    {
        Position = GameObject.Find("Player_0").transform;
    }

    private void LateUpdate() // LateUpdate is used, because it is called after Update and FixedUpdate, so that the camera only moves after the player moved.
    {
        Vector3 deltaCamera = Vector3.zero;
        float x;
        float y;

        // Check if position is in the x-axis bound
        float deltaX = Position.position.x - transform.position.x;
        if (deltaX > maxX || deltaX < -maxX)
        {
            if (transform.position.x < Position.position.x) //If true the player is to the right of the cameras center.
            {
                deltaCamera.x = deltaX - maxX;
            }
            else
            {
                deltaCamera.x = deltaX + maxX;
            }
        }

        // Check if position is in the y-axis bound
        float deltaY = Position.position.y - transform.position.y;
        if (deltaY > maxY || deltaY < -maxY)
        {
            if (transform.position.y < Position.position.y) // If true the player is to the right of the cameras center.
            {
                deltaCamera.y = deltaY - maxY;
            }
            else
            {
                deltaCamera.y = deltaY + maxY;
            }
        }

        x = deltaCamera.x;
        y = deltaCamera.y;
        x = System.Convert.ToSingle(System.Math.Round(x,3));
        y = System.Convert.ToSingle(System.Math.Round(y,3));



        // Move the camera
        transform.position += new Vector3(x, y, 0); 
    }
}
