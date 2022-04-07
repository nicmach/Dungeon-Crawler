using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : CollideCharacters
{

    public string[] sceneNames;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player_0")
        {
            GameManager.instance.Save();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)]; // Saves a random scene in sceneName to use it in the next line
            SceneManager.LoadScene(sceneName);
        } 
    }
}
