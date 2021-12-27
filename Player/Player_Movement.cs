using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    GameObject child;

    //sideway run
    float Speed = 0;
    public float MaxSpeed = 6f;
    public float Acceleration = 7f;
    public float Deceleration = 7f;

    //sideway walk
    public float walkSpeed = 3f;

    //jump
    public float jumpVelocity = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //check hitting ground and wall
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    void Start()
    {
        child = GameObject.Find("Square");
        body = transform.GetComponent<Rigidbody2D>();
        boxCollider = child.GetComponent<BoxCollider2D>();
    }   
    void Update()
    {
        Move();
        Jump();
        Fall();
        Rotate();
    }

    //sideway move
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            Speed = walkSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            Speed = -walkSpeed;
        }
        else if ((Input.GetKey(KeyCode.D)) && (Speed < MaxSpeed))
        {
            Speed = Speed + Acceleration * Time.deltaTime;
        }
            
        else if ((Input.GetKey(KeyCode.A)) && (Speed > -MaxSpeed))
        {
            Speed = Speed - Acceleration * Time.deltaTime;
        }
           
        else
        {
            if (Speed > Deceleration * Time.deltaTime && !Input.GetKey(KeyCode.LeftShift))
                Speed = Speed - (Deceleration + 1) * Time.deltaTime;
            else if (Speed < -Deceleration * Time.deltaTime && !Input.GetKey(KeyCode.LeftShift))
                Speed = Speed + (Deceleration + 1) * Time.deltaTime;
            else
                Speed = 0;
        }
        transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
    }

    //jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            body.velocity = Vector2.up * jumpVelocity;
        }
        
    }

    //falling down control
    private void Fall()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //checking if player is grounded or on wall
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //rotate sideways
    void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1,1,1);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
