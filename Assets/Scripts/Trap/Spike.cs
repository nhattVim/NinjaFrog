using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float atk;
    public float damageDelay = 0.5f;
    protected float lastDamageTime;
    private Player player;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                if (Time.time >= lastDamageTime + damageDelay)
                {
                    player.TakeDamage(atk);
                    Debug.Log("Enemy dealt damage: " + atk);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

}
