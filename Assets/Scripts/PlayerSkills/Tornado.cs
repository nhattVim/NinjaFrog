using System.Collections;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float manaCost = 10f;
    public float damage = 10f;
    public float damageDelay = 1.0f;
    private float lastDamageTime;
    public float moveSpeed = 4f;
    public float lifetime = 5f;
    private Vector2 moveDirection;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAfterLifetime());
    }

    void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));

            if (isGrounded)
            {
                transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += (Vector3)(new Vector2(0, -1) * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                collision.GetComponent<BaseEnemy>()?.TakeDamage(damage);
                Debug.Log($"Kẻ địch nhận sát thương: {damage}");
                lastDamageTime = Time.time;
            }
        }
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime - 0.5f);
        animator.SetTrigger("End");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
