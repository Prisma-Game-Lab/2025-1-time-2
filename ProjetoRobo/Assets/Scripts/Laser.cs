using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private Vector2 direction;
    private int damage;
    private float speed = 5f;
    private float laserLifetime;

    void Awake() 
    {
        //transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);;
    }

    void Update() 
    {
        //transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetUp(Vector2 dir, int laserdamage, float laserSpeed, float lifetime, LayerMask layerMask) 
    {
        direction = dir;
        damage = laserdamage;
        speed = laserSpeed;
        laserLifetime = lifetime;
        rb = GetComponent<Rigidbody2D>();

        rb.includeLayers = layerMask;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Vector2.SignedAngle(Vector2.up, dir));
        rb.velocity = dir * speed;

        StartCoroutine(DisableAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    
            
            {
             DamageController damageController = collision.GetComponent<DamageController>();
        if (damageController != null) 
        {
            damageController.OnDamage(damage);
        }
        }
    
        gameObject.SetActive(false);

       
        
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(laserLifetime);
        gameObject.SetActive(false);
    }
}