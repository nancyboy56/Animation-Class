using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class BasicJump : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpHeight = 7;
    private bool jumping = false;
    private bool ceiling = false;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumping = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumping)
        {
            // ForceMode2D.Impulse means that all force is applied in one hit not over time
            rb.AddForce((Vector2.up) * jumpHeight, ForceMode2D.Impulse);
            jumping = false;

        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        
        if (context.action.name =="Jump" && context.performed)
        {
            print("Jump");
            jumping = true;
        }

        
    }


}
