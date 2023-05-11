using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpingPower;
    public int health;
    public int knockBack;
    public int playerDamage;
    public bool isFacingRight = true;
    public Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    List<Enemy> availableEnemies = new List<Enemy>();

    //If enemy enters the attack range, the enemy script is added to a list

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Entered!");
            availableEnemies.Add(col.GetComponent<Enemy>());
        }
    }

    //If enemy exits the attack range, the enemy script is removed from the list
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited");
        if (other.gameObject.tag == "Enemy")
        {
            availableEnemies.Remove(other.GetComponent<Enemy>());
        }
    }

    void Start()
    {
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", speed);

        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Iterates through list of enemies and deals damge to each.
    void Attack()
    {
        Enemy theEnemy;
        for (int i = 0; availableEnemies.Count > i; i++)
        {
            if (isFacingRight == true)
            {
                Debug.Log("Attacking " + i);
                theEnemy = availableEnemies[i];
                theEnemy.GetComponentInParent<Rigidbody2D>().AddForce((transform.right + transform.up) * knockBack);
                theEnemy.TakeDamage(playerDamage);
            }
            else
            {
                Debug.Log("Attacking " + i);
                theEnemy = availableEnemies[i];
                theEnemy.GetComponentInParent<Rigidbody2D>().AddForce((-transform.right + transform.up) * knockBack);
                theEnemy.TakeDamage(playerDamage);
            }
        }
    }
}