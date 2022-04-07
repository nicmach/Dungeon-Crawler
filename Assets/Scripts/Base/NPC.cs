using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : CollideCharacters
{
    public string message;
    private float cooldown = 5.0f;
    private float lastShown;

    protected override void Start()
    {
        base.Start();
        lastShown = 0 - cooldown;
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player_0" && Time.time - lastShown > cooldown)
        {
            lastShown = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position, Vector3.zero, cooldown);
        }
    }
}
