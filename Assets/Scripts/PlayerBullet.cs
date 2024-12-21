using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;
    private Vector3 direction;
    private Coroutine returnCoroutine;
    private ObjectPool pool;

    public void SetDirection(Vector3 direction, ObjectPool pool)
    {
        this.direction = direction.normalized;
        this.pool = pool;

        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(ReturnToPoolAfterDelay(5f));
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if (tag == "Enemies")
        {
            collision.GetComponent<BaseEnemy>()?.TakeDamage(damage);
            Debug.Log($"Enemy takes damage {damage}");
            ReturnToPool();
        }
        else if (tag != "Player" && tag != "Ground" && tag != "Cherries" && tag != "EnemyBullet" && tag != "Sword" && tag != "Item" && tag != "BossSkill")
        {
            ReturnToPool();
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        pool.ReturnObject(gameObject);
    }
}
