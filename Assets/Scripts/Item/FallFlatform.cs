using UnityEngine;
using System.Collections;

public class FallFloatform : MonoBehaviour
{
    public float fallDelay = 1f;
    public float destroyTime = 5f;
    private Rigidbody2D rg;
    private bool isDestroyed = false;

    public bool IsDestroyed => isDestroyed;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.bodyType = RigidbodyType2D.Kinematic;
        rg.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndDestroy());
        }
    }

    private IEnumerator FallAndDestroy()
    {
        yield return new WaitForSeconds(fallDelay);
        rg.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyTime);
        isDestroyed = true;
        rg.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void ResetPlatform()
    {
        isDestroyed = false;
        rg.bodyType = RigidbodyType2D.Kinematic;
        rg.velocity = Vector2.zero;
    }
}
