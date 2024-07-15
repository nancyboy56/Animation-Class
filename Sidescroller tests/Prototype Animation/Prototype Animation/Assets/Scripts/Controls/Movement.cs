using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float jumpHeight = 7;

    [SerializeField]
    private int maxJump = 3;

    [SerializeField]
    private int groundLayer = 9;

    [SerializeField]
    private int raycastDistance = 1;

    [SerializeField]
    private float moveDown = -1;

    [SerializeField]
    private float moveSpeed = 1;

    [SerializeField]
    private float fallSpeed = 3;

    [SerializeField]
    private float lowJumpSpeed = 3;

    private SpriteRenderer sr;

    //this is not good code!!!
    //change later!!!
   /* public SpriteRenderer headSr;
    public SpriteRenderer bootsSr;
    public SpriteRenderer hairSr;
    public SpriteRenderer collarSr;*/


    private bool jumping = false;
    private bool isMoving = false;
    private int jumpCount = 0;
    private Rigidbody2D rb;
    private InputAction.CallbackContext context;
    private bool holding = false;
    private int called = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down *raycastDistance, Color.yellow);
        
    }

    void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, transform.position + Vector3.down, Color.green);
        if (isMoving)
        {
            MoveWithPosition();
            FlipSprite();
            print("moving");

        }

        if (jumping)
        {
            BasicJump();
        }
        FastFall();
        HoldingJump();

    }

    private void MoveWithVelocity()
    {

        rb.AddForce(new Vector3(context.ReadValue<Vector2>().x, 0) * moveSpeed * Time.deltaTime);

        //print(rb.velocity);
    }

    private void MoveWithPosition()
    {
        if (GroundedRaycast().collider == null)
        {
            rb.MovePosition(transform.position + new Vector3(context.ReadValue<Vector2>().x, moveDown).normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(transform.position + new Vector3(context.ReadValue<Vector2>().x, 0) * moveSpeed * Time.deltaTime);
        }
        
    }

    // dont like this function 
    // bad code!!
    //cahnge assets to always be one
    private void FlipSprite()
    {
        float x = context.ReadValue<Vector2>().x;
        if (x < 0){
            sr.flipX = true;
           /* headSr.flipX = true;
            hairSr.flipX = true;
            bootsSr.flipX = true;
            collarSr.flipX = true;*/
        }
        else
        {
            sr.flipX = false;
          /*  headSr.flipX = false;
            hairSr.flipX = false;
            bootsSr.flipX = false;
            collarSr.flipX = false;*/
        }
        
        
    }

    private void FastFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity +=Vector2.up * Physics2D.gravity.y * fallSpeed * Time.deltaTime;

           // rb.velocity.y = 0;
        } 
    }

    private void HoldingJump()
    {
        if(rb.velocity.y>0 && holding == false)
        {
            
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpSpeed * Time.deltaTime;
        }
    }

    private void BasicJump()
    {
        jumpCount++;
        // ForceMode2D.Impulse means that all force is applied in one hit not over time
        //could devide by zero so idk
        //might change to just an array
        rb.AddForce((Vector2.up) * (jumpHeight / jumpCount), ForceMode2D.Impulse);
        jumping = false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            holding = !holding;
            print("jump started");

            RaycastHit2D hit = GroundedRaycast();

            if (hit.collider != null && jumpCount < maxJump)
            {

                jumping = true;
                jumpCount = 0;

            }
            else if (jumpCount > 0 && jumpCount < maxJump)
            {
                jumping = true;
            }
            else
            {

                jumpCount = 0;
            }
        }
        else if ( context.performed)
        {
            print("jump performed");
           /* called++;

            print("called" + called);*/

            

        } else if (context.canceled)
        {
            holding = !holding;
            print("jump cancelled");
        }

       // print("Holding: " + holding);
    }

    //When WASD is pressed the player moves and when WASD is released the player stops
    public void Move(InputAction.CallbackContext newContext)
    {
        
        context = newContext;
        if (context.started)
        {
            RaycastHit2D hit = GroundedRaycast();
            if (hit.collider != null || jumpCount == 1)
            {
                isMoving= !isMoving;
                print("Move pressed");
            }
        }
        else if (newContext.performed )
        {
           
        } else if (newContext.canceled)
        {
            isMoving = false;
        }
        
    }

    //Checks if the
    private RaycastHit2D GroundedRaycast()
    {
        //print("drawing");
        int layerMask = 1 << groundLayer;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), raycastDistance, layerMask);
        
        return ray;
            

    }
}
