using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float ?BulletSpeed { private get; set; }
    public int ?Direction { private get; set; } // 0 down, 1 up, 2 or 3 side.
    public float ?Damage { private get; set; }

    public GameObject Owner { private get; set; }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (Direction == null || Damage == null || Owner == null)
        {
            Debug.Log("bullet failed,  direction not set and  == null");
            return;
        }
        switch (Direction)
        {
            case 0:
                rb.velocity = (Vector2)(BulletSpeed * -transform.up);
                return;
            case 1:
                rb.velocity = (Vector2)(transform.up * BulletSpeed);
                return;
            case 2:
            case 3:
                rb.velocity = (Vector2)(BulletSpeed * -transform.right);
                return;
            default:
                Debug.Log("direction int not recognised");
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Owner == null)
        {
            Debug.Log("Owner collision null");
            return;
        }
        if (collision.gameObject == null)
        {
            Debug.Log("collision target == null");
            return;
        }
        if (!Owner.CompareTag(collision.tag))
        {
            if (collision.CompareTag("Enemy"))
            {
                HealthController hC = collision.GetComponent<HealthController>();
                hC.ApplyDamage((float)Damage);
                // bullet impact animation,
                Destroy(gameObject);

            }
            if (collision.CompareTag("Player"))
            {
                HealthController hC = collision.GetComponent<HealthController>();
                hC.ApplyDamage((float)Damage);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("owner/friendly Collision");
        }
        
    }
}
