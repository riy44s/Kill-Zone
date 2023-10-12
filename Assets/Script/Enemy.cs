using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
  
    private Animator anim;
    public GameObject Bullet;
    public Transform BulletPos;

    public float Timer;
    private GameObject player;
    private bool isEnemyDead = false;
    private BoxCollider2D boxCollider;

    [Header ("Health")]
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    public static int enemyKillCount;
    public Text killCount;
    public bool isCounted = false;

    public float distanceCount=45f;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyKillCount = 0;
    }

    public void TakeDamage(int damage)
    {
        if (isEnemyDead)
            return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            isEnemyDead = true;         
            anim.SetTrigger("Die");
            healthBar.gameObject.SetActive(false);
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
            EnemyPatrol enemyPatrol = GetComponentInParent<EnemyPatrol>();
            if (enemyPatrol != null)
            {
                enemyPatrol.enabled = false;
            }
            Destroy(gameObject,6f);

            if (!isCounted)
            {
                enemyKillCount++;
                killCount.text = enemyKillCount.ToString();
                isCounted = true;
            }
           
        }
    }

    private void Update()
    {
        if (isEnemyDead)
            return;

        Timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
       //Debug.Log(distance);
        if (distance < distanceCount)
        {
            Timer += Time.deltaTime;

            if (Timer > 2)
            {
                Timer = 0;
                Shoot();
            }
        }

       
    }
    void Shoot()
    {
        Instantiate(Bullet, BulletPos.position, Quaternion.identity);
    }
}
