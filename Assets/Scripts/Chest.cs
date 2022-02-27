using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable // Chest inheritates from Collectable and therefor also from ColliderCharacters (and so on), but can change functions from it, because they are virtual (that should also work with abstract - I think ...).
{
    public Sprite Chest_01;
    public int Gold = 10;
    protected override void OnCollide(Collider2D coll)
    {
       if (!collected && coll.name == "Player_0")
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = Chest_01;     // Change the sprite to the one with the empty chest
            GameManager.instance.ShowText("You get " + Gold + " Gold", 25, new Color(255f, 215f, 0f) , transform.position /* gives the position of the chest*/, Vector3.up * 40, 1.5f);
            GameManager.instance.gold += Gold;
        }
        
    }
}
