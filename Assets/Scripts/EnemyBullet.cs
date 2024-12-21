using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 10f;
    private Vector3 direction;

    public void SetDirection(Vector3 direction, float destroyTime)
    {
        this.direction = direction.normalized;
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if (tag == "Player")
        {
            collision.GetComponent<Player>()?.TakeDamage(damage);
            Debug.Log($"Player nhận damage: {damage}");
            Destroy(gameObject);
        }
        else if (tag != "Enemies" && tag != "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
