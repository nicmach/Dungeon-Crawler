using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform projectilePoint;
    public GameObject fireball;
    public float delay = 1.5f;
    private float lastShot;

    public float shootForce = 2.5f;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastShot > delay)
        {
            lastShot = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        Quaternion newRotation = projectilePoint.rotation * Quaternion.Euler(0, 0, 90);
        GameObject Fireball_0 = Instantiate(fireball, projectilePoint.position, newRotation);
        Rigidbody2D rb = Fireball_0.GetComponent<Rigidbody2D>();
        rb.AddForce(projectilePoint.up * shootForce, ForceMode2D.Impulse);
    }

}
