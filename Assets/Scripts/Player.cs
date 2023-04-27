using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    public int health;
    public int knockBack;
    public int playerDamage;
    private bool isFacingRight = true;
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
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    //Iterates through list of enemies and deals damge to each.
    void Attack()
    {
        Enemy theEnemy;
        for (int i = 0; availableEnemies.Count > i; i++)
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                Debug.Log("Attacking " + i);
                theEnemy = availableEnemies[i];
                theEnemy.GetComponentInParent<Rigidbody2D>().AddForce((transform.right + transform.up) * knockBack);
                theEnemy.TakeDamage(playerDamage);
            }
            else
            {
                isFacingRight = !isFacingRight;
                Debug.Log("Attacking " + i);
                theEnemy = availableEnemies[i];
                theEnemy.GetComponentInParent<Rigidbody2D>().AddForce((-transform.right + transform.up) * knockBack);
                theEnemy.TakeDamage(playerDamage);
            }
        }
    }
}
