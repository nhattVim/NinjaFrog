using System.Collections;
using UnityEngine;

public class EnergyBlast : MonoBehaviour
{
    public float manaCost = 10f;
    public float initialDamage = 10f;
    public float explosionDamage = 1f;
    public float explosionDamageDelay = 1.0f;
    private float lastExplosionDamageTime;
    public float explosionDuration = 1.0f;
    public float moveSpeed = 10f;
    public float lifetime = 5f;
    private Vector3 moveDirection;
    private Animator animator;
    private bool isExploding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector3 direction)
    {
        this.moveDirection = direction.normalized;
    }

    void Update()
    {
        if (!isExploding)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExploding)
        {
            ApplyDamage(collision, initialDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isExploding && Time.time >= lastExplosionDamageTime + explosionDamageDelay)
        {
            ApplyDamage(collision, explosionDamage);
        }
    }

    private void ApplyDamage(Collider2D collision, float damage)
    {
        string tag = collision.tag;

        if (tag == "Enemies")
        {
            collision.GetComponent<BaseEnemy>()?.TakeDamage(damage);
            Debug.Log($"Kẻ địch nhận sát thương: {damage}");
            lastExplosionDamageTime = Time.time;
            StartCoroutine(TriggerExplosion());
        }
        else if (tag != "Player" && tag != "Ground" && tag != "Cherries" && tag != "EnemyBullet" && tag != "Sword" && tag != "Item" && tag != "BossSkill")
        {
            StartCoroutine(TriggerExplosion());
        }
    }

    IEnumerator TriggerExplosion()
    {
        isExploding = true;
        animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
}
