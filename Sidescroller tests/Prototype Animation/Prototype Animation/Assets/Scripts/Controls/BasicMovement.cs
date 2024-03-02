using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 1;

    private bool isMoving = false;
    private InputAction.CallbackContext context;

    [SerializeField]

    private int called = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (isMoving)
        {
            MoveWithPosition();
           // print("moving");

        }else
        {
            //print("not moving");
        }

    }

    private void MoveWithVelocity()
    {
        
        rb.AddForce(new Vector3(context.ReadValue<Vector2>().x, 0) * moveSpeed * Time.deltaTime);
        
        //print(rb.velocity);
    }

    private void MoveWithPosition()
    {
        rb.MovePosition(transform.position + new Vector3(context.ReadValue<Vector2>().x, 0) * moveSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext newContext)
    {
        ++called;
        context = newContext;
        if(newContext.action.name== "Move")
        {
            if (newContext.performed)
            {
                isMoving = !isMoving;
                print("Move pressed");

            }
            else
            {
                isMoving = !isMoving;
                //print("not performed");
            }

        }
        
        
        

       
        //isMoving = true;
        
        
        
        /*print("Move");
        rb.velocity+= context.ReadValue<Vector2>() * moveSpeed;*/

        //rb.velocity = moveSpeed;
    }
}
