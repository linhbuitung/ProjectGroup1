using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Zombie_Movement : MonoBehaviour
{
    public float chaseSpeed;
    public float detectRange;
    public float idleMoveRange;
    private float speed;
    private bool isChasing = false;
    private bool isIdle = false;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    public Rigidbody2D player;
    [SerializeField] private LayerMask groundLayer;
    Vector3 idlePos;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        Idle();
    }

    void Idle()
    {
        if(isChasing == false && isIdle == false)
        {
            speed = chaseSpeed / 3;
            idlePos = transform.position;
            isIdle = true;
        } else if (isChasing == false && isIdle == true)
        {           
            if (transform.position.x > idlePos.x + idleMoveRange)
            {
                speed = - chaseSpeed / 3; 
            } else if (transform.position.x < idlePos.x - idleMoveRange)
            {
                speed = chaseSpeed / 3;
            }
            IdleRotate(speed);
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);            
        }
        
    }
    void Chase()
    {
        if (isGrounded() && (Vector2.Distance(transform.position, player.position) < detectRange) && isChasing == false && isIdle == true)
        {
            Rotate();
            isChasing = true;
            isIdle = false;
            if (player.position.x > body.position.x)
            {
                speed = chaseSpeed;
            }
            else if (player.position.x < body.position.x)
            {
                speed = -chaseSpeed;
            }
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        } else
        {            
            isChasing = false;
        }
    }

   
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void Rotate()
    {
        if (player.position.x < body.position.x)
        {
            transform.localScale = new Vector3( - scale.x, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    void IdleRotate(float speed)
    {
        if (speed < 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        else if ( speed >0 )
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }
}
