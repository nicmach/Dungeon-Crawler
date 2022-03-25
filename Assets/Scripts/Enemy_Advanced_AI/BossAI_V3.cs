using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI_V3 : EnemyAI_V3
{
    public float[] minisSpeed = { };
    public float distance = 0.5f;
    public Transform[] minis;

    private void Update()
    {
        for (int i = 0; i < minis.Length; i++)
        {

            minis[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * minisSpeed[i]) * distance, Mathf.Sin(Time.time * minisSpeed[i]) * distance, 0);
        }
    }
}
