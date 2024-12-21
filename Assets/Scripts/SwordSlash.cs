using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class SwordSlash : MonoBehaviour
{
    private float atk = 1f;
    public Transform attackPoint;
    public LayerMask attackLayer;
    public Vector2 attackSize = new Vector2(1f, 2f);
    private GameObject playerObject;
    private Player playerScript;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetDamage(float newAtk)
    {
        atk = newAtk;
    }

    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0f, attackLayer);
        foreach (var coll in colliders)
        {
            coll.GetComponent<BaseEnemy>()?.TakeDamage(atk);
            Debug.Log($"Kẻ địch {coll.name} nhận damage {atk}");
            Variables.Object(playerObject).Set("currentMana", playerScript.currentMana += 1);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }
}
