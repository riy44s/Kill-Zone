using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed = 20f;
    [SerializeField] Rigidbody2D rb;

    private PlayerController playerController;
    public ParticleSystem impactEffect;
    public ParticleSystem deathEffect;
    public int damage = 40;
    void Start()
    {   
        playerController = FindObjectOfType<PlayerController>();  
        Vector2 initialDirection = playerController.IsFacingRight() ? Vector2.right : Vector2.left;
        rb.velocity = initialDirection * Speed;

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
      Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Die();
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
