using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableWeapons : Collectable
{
    public Item item;
    public string sceneName;
    public int WeaponID;

    //public string WeaponName;
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
        if (GameManager.instance.Weapons.Contains(WeaponID) && scene.name == sceneName)
        {
            Destroy(gameObject);
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player_0")
        {
            PickUp();
        } 

    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            GameManager.instance.ShowText("You found " + item.name, 25, new Color(255f, 215f, 0f), transform.position  /*gives the position of the chest*/, Vector3.up * 40, 1.5f);
            GameManager.instance.Weapons.Add(WeaponID);
            Destroy(gameObject);
        }
    }
}

