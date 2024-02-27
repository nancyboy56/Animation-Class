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
    
    // Start is called before the first frame update
    void Start()
    {
        //print("Start Movement!");
        rb =GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(isMoving)
        {
            rb.MovePosition(transform.position + new Vector3(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y) * moveSpeed *Time.deltaTime);
        }
    }

    public void Move(InputAction.CallbackContext newContext)
    {
        isMoving = true;

        context= newContext;
        /*print("Move");
        rb.velocity+= context.ReadValue<Vector2>() * moveSpeed;*/

        //rb.velocity = moveSpeed;
    }
}
