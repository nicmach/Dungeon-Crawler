using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>(); // A list of element with the class FloatingText which was created in FloatingText


    private void Update() // Call the UpdateFloatingText function to update the floating text
    {
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }
    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        // Creating a floatingText GameObject with the helpt of the FloatingText class
        FloatingText floatingText = GetFloatingText(); 

        floatingText.txt.text = message; //.txt.text is the text variable inside the txt object with which you change the text
        floatingText.txt.fontSize = fontSize; // same as above for the fontSize
        floatingText.txt.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // Transfers the world space to screen space, so that it can be used in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }
    private FloatingText GetFloatingText()
    {
        FloatingText text = floatingTexts.Find(i => !i.active); // Looks for a FloatingText in the List floatingTexts, which is not active
        /* The notation above is comparable to a for loop, which iterates over all Elements
         * in floatingTexts and checks, wheter they are active or not.
         */

        if (text == null) // If we do not find an inactive FloatingText
        {
            text = new FloatingText(); // Create a new FloatingText
            text.go = Instantiate(textPrefab); // Creating a new GameObject and assigning it to text.go
            text.go.transform.SetParent(textContainer.transform); // Parent of this new GameObject is the transform of textContainer
            text.txt = text.go.GetComponent<Text>();

            floatingTexts.Add(text);
        }

        return text; // If we find an inactive FloatingText we return it
    }
}
