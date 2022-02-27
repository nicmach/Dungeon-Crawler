using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : CollideCharacters
{
    // Weapon stats
    public float[] damage = {1, 2, 3, 3.5f, 4, 5, 6.5f, 8};
    public float[] knockback = {1, 1.25f, 1.5f, 1.5f, 2f, 2f, 2.25f, 2.75f};

    // Weapon Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Attack
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start(); // Alternative one could use the code within Start
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "Player_0")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage[weaponLevel],
                origin = transform.position,
                knockback = knockback[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Attack"); // The trigger, which activates the sword swing animation
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
