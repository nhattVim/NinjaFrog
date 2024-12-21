using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public EnemyData enemyData;

    [Header("Enemy Status")]
    public float HP;
    public float atk;
    public float speed;
    public float damageDelay = 1.0f;

    [Header("Other")]
    public GameObject HPPotion;
    public GameObject ManaPotion;
    public bool isFacingRight = true;
    public float detectionRadius = 5f;
    protected float lastDamageTime;

    protected Player playerScript;
    protected Transform playerTransform;
    protected Animator ani;

    private void Awake()
    {
        if (enemyData != null)
        {
            HP = enemyData.hp;
            atk = enemyData.atk;
            speed = enemyData.speed;
        }
        else
        {
            Debug.LogError("EnemyData is missing!. Set default status");
            HP = 100f;
            atk = 10f;
            speed = 5f;
        }

        ani = GetComponent<Animator>();
        if (ani == null)
        {
            Debug.Log("Missing animation for enemy");
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.Log("Missing player tranform for enemy");
        }
    }

    public virtual void TakeDamage(float damage)
    {
        HP -= damage;
        ani.SetTrigger("Hurt");
        if (HP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy has died.");
        if (Random.Range(0, 100) < 25)
        {
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(HPPotion, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(ManaPotion, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject, 0.1f);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                collision.gameObject.GetComponent<Player>()?.TakeDamage(atk);
                Debug.Log("Kẻ địch gây sát thương: " + atk);
                lastDamageTime = Time.time;
            }
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
