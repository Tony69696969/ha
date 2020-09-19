using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float horizontalMove;
    public float moveSpeed;
    public float jumpForce;
    public bool jumpPressed;
    int jumpCount;
    public bool isOnGround;
    public float leftFootOffsetX;
    public float leftFootOffsetY;
    public float rightFootOffsetX;
    public float rightFootOffsetY;
    public float groundDistance;
    public LayerMask groundLayer;
    PlayerAnimation playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")&&jumpCount>0)
        {
            jumpPressed = true;
        }
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();
        FilpDirection();
    }
    void GroundMovement()
    {
        rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
    }
    void FilpDirection()
    {
        if(horizontalMove!=0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }
    void MidAirMovement()
    {
        if (isOnGround)
        {
            jumpCount = 2;
        }
        if (jumpPressed && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }
    
    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(leftFootOffsetX, leftFootOffsetY), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(rightFootOffsetX, rightFootOffsetY), Vector2.down, groundDistance, groundLayer);
        if(leftCheck||rightCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
    RaycastHit2D Raycast(Vector2 offset,Vector2 rayDiration,float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiration, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos + offset, rayDiration * length,color);
        return hit;
    }
}
