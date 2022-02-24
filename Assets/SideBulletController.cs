using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet Collision Detected");
        
        if (collision.tag == "Enemy")
        {
            // damage enemy
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        /*if (collision.tag == "Player")
        {
            // damage enemy
        }*/ // use if implement ricochets;
    }


}
