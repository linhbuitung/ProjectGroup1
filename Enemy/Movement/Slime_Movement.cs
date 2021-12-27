using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Movement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private int timeBetween;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectRange;
    //private Animator anim;
    private CircleCollider2D circleCollider;
    private Rigidbody2D body;
    public Rigidbody2D player;
    private float realmoveSpeed;
    Enemy_Controller enemyTracker;
    Vector3 scale;


    void Start()
    {
        StartCoroutine("Jump");
        body = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        enemyTracker = body.GetComponent<Enemy_Controller>();
        scale = transform.localScale;
    }

    private void Update()
    {
        //anim.SetBool("Grounded", isGrounded());
        Rotate();
        CheckDeath();
    }

    private IEnumerator Jump()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetween);
            if (isGrounded() && (Vector2.Distance(transform.position, player.position) < detectRange))
            {
                //anim.SetTrigger("jump");
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                Chase();
            }

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    void Chase()
    {
        if (player.position.x > body.position.x)
        {
            realmoveSpeed = 10 * moveSpeed;
        }
        else
        {
            realmoveSpeed = -10 * moveSpeed;
        }
        body.AddForce(new Vector2(realmoveSpeed, 0));
    }
    private void Rotate()
    {
        if (player.position.x < body.position.x)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    void CheckDeath()
    {
        if (enemyTracker.isDead == true)
        {
            StopCoroutine("Jump");
        }
    }
}
