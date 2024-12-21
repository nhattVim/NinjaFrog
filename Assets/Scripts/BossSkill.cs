using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public float atk = 12f;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.GetComponent<Player>()?.TakeDamage(atk);
            Debug.Log("Skill Boss gây sát thương: " + atk);
        }
    }
}
