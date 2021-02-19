using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D enemyRigidBody;
    private Enemy_Stats stats;
    public GameObject player;
    public float m_Speed;
    public bool attack = false;

    public string attackAnimationString;
    public string movementAnimationString;
    public bool goingLeft, cameIntoContactWithPlayer;

    void Start()
    {
        stats = GetComponent<Enemy_Stats>();
        animator = GetComponent<Animator>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        if (!goingLeft)
            Flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameIntoContactWithPlayer)
        {
            if (player.transform.position.x > this.transform.position.x && goingLeft)
            {
                Flip();
                goingLeft = false;
            }
            if (player.transform.position.x < this.transform.position.x && !goingLeft)
            {
                Flip();
                goingLeft = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!attack)
        {
            animator.SetTrigger(movementAnimationString);
            SetMovementSpeed(m_Speed);
            animator.SetBool("isAttacking", attack);
        }

        else
        {
            SetMovementSpeed(0.0f);
            animator.SetTrigger(attackAnimationString);

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameIntoContactWithPlayer = true;
            attack = true;
            animator.SetBool("isAttacking", attack);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack = false;
            Debug.Log("Found the Player");
            animator.SetBool("isAttacking", attack);
        }
    }

    void SetMovementSpeed(float speed)
    {
        Vector2 enemyVol = enemyRigidBody.velocity;
        enemyVol.x = speed;
        if (goingLeft)
        {
            enemyRigidBody.velocity = -enemyVol;
        }
        else
        {
            enemyRigidBody.velocity = enemyVol;
        }

    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void IsWeaponActive(int value)
    {
        if (value == 1)
            player.GetComponent<Player_Stats>().ApplyDamage(stats.BaseAttack);
    }
}
