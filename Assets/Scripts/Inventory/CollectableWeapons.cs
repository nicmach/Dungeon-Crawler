using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableWeapons : Collectable
{
    public string WeaponName;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.instance.Weapons == 1 && scene.name == "PlayerBase")
        {
            Destroy(gameObject);
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player_0")
        {
            GameManager.instance.ShowText("You found " + WeaponName, 25, new Color(255f, 215f, 0f), transform.position /* gives the position of the chest*/, Vector3.up * 40, 1.5f);
            GameManager.instance.Weapons = 1;
            Destroy(gameObject);
        }

    }
}
