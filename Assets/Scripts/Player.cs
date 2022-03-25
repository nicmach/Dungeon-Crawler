using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving
{
    private SpriteRenderer spriteRenderer;
    private bool playerAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>(); //Get an component of type Sprite Renderer
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (playerAlive)
            base.ReceiveDamage(dmg);
    }

    protected override void Death()
    {
        playerAlive = false;
        GameManager.instance.DeathWindow.SetTrigger("Shown");
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        playerAlive = true;
        lastImmune = Time.time; // To prevent knockback etc. from an attack before death (refer to Fighter.cs)
    }

    private void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (playerAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
            GameManager.instance.OnHitpointChange();
        }
    }

    public void SwapSprite(int spriteID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[spriteID]; // Assign the SpriteRenderer sprite component the skin we want
    }

    public void OnLevelUp()
    {
        int currLevel = GameManager.instance.GetCurrentLevel();
        maxHitpoint = System.Convert.ToInt32(System.Math.Ceiling(maxHitpoint * 1.1));
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healing)
    {
        hitpoint += healing;

        if (hitpoint >= maxHitpoint)
        {
            hitpoint = maxHitpoint;
            return;
        }

        GameManager.instance.ShowText("+" + healing.ToString() + "hp", 25, new Color(0f, 179f, 0f), transform.position, Vector3.up * 40, 1.5f);
    }
}
