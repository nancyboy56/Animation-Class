using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class BasicJump : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpHeight = 7;
    private bool jumping = false;
    private bool ceiling = false;
    private bool grounded = true;

    [SerializeField]
    private int maxJump = 3;

    [SerializeField]
    private int jumpCount = 0;

    [SerializeField]
    private int groundLayer;

    [SerializeField]
    private int raycastDistance = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumping = false;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * raycastDistance, Color.yellow);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumping)
        {

            Jump();
            

        }

        /*if (isMoving)
        {
            MoveWithPosition();
            print("moving");

        }*/


    }

    private void Jump()
    {
        jumpCount++;
        // ForceMode2D.Impulse means that all force is applied in one hit not over time
        //could devide by zero so idk
        //might change to just an array
        rb.AddForce((Vector2.up) * (jumpHeight /jumpCount), ForceMode2D.Impulse);
        jumping = false;
    }

    public void Jump(InputAction.CallbackContext context)
    {

        if (context.action.name == "Jump" && context.performed)
        {
            int layerMask = 1 << groundLayer;



            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), raycastDistance, layerMask);


            if (hit.collider != null && jumpCount < 3)
            {
                jumping = true;
                jumpCount = 0;
                print("Jump");
            }
            else if (jumpCount > 0 && jumpCount < maxJump)
            {
                jumping = true;
            }
            else
            {
                print("dont jump");
                jumpCount = 0;
            }
    
            //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit);
            
           // Physics.Raycast(__instance.transform.position + __instance.transform.forward, Vector3.up, out raycastHit, 2f, LayerMaskDefaults.Get(LMD.Environment)))
        }
    }
}



