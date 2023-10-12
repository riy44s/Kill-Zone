using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player")]
    private float horizontal;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Animator anim;

    [Header ("Health")]
    public int currentHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    Vector2 checkPointPos;

    [Header ("Lives")]
    public TextMeshProUGUI livesText;
    public int livesCount = 3;
    private bool hasRespawned = false;

    public GameManeger gm;
    private Weapon weapon;

    public GameObject respawnParticle;

    private bool isJumping = false;
    private void Start()
    {
        gm = FindObjectOfType<GameManeger>();
        weapon = FindObjectOfType<Weapon>();
        checkPointPos = transform.position;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = SimpleInput.GetAxisRaw("Horizontal");
        anim.SetBool("Run", Mathf.Abs(horizontal) > 0);

        if (IsGround())
        {
           
            if (Input.GetButtonDown("Jump"))
            {
                Jump(); 
            }

          
            if (isJumping)
            {
                anim.SetBool("jump", false);
                isJumping = false;
            }
        }
        else
        {
           
            anim.SetBool("jump", true);
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }

    public void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y *= -1f;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void Jump()
    {
        if (IsGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("jump", true);
            isJumping = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AudioManeger.Instance.PlaySFX("Hit");
        anim.SetTrigger("Hit");
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            anim.SetBool("Death", true);
            Respawn();
            enabled = false;
            weapon.enabled = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Health")
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            AudioManeger.Instance.PlaySFX("Collect");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            gm.FinishPoint();
        }
        else if (collision.gameObject.CompareTag("Ammo"))
        {
            weapon.AmmoRefill();
            Destroy(collision.gameObject);
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    public void Respawn()
    {
        if (!hasRespawned)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    IEnumerator RespawnCoroutine()
    {
        hasRespawned = true;
        yield return new WaitForSeconds(4f);
        weapon.playerRespawnAmmoRefill();
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        anim.SetBool("Death", false);
        enabled = false;
        transform.position = checkPointPos;
        RespwanParticle();
        yield return new WaitForSeconds(2f);
        if (hasRespawned)
        {
            livesCount--;
            UpdateLivesUI();
        }

        hasRespawned = false;
        enabled = true;
        weapon.enabled = true;
        if (livesCount <= 0)
        {
            AudioManeger.Instance.musicSource.Stop();
            AudioManeger.Instance.PlaySFX("Death");
            gm.GameOver();
        }
    }

    private void UpdateLivesUI()
    {
        livesText.text = livesCount.ToString();
    }

    void RespwanParticle()
    {
        Instantiate(respawnParticle, transform.position, Quaternion.identity);
    }
}
