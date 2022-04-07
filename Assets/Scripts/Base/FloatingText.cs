using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time; // Save the last time it was shown
        go.SetActive(active); // Activate the connected GameObject depending on the input (wheter it is true or false)
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active); // Hide the object
    }

    public void UpdateFloatingText()
    {
        if (!active) // If the GameObject is not active there is no need to do anything
            return;

        if (Time.time - lastShown > duration) // Will hide the text after a given amount of time (CurrentTime - TimeItWasLastShown must be bigger than the duration)
            Hide();

        go.transform.position += motion * Time.deltaTime; // Move the text 
    }
}
