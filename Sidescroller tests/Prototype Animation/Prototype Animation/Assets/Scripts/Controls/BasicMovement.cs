using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 1;
    
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

    public void Move(InputAction.CallbackContext context)
    {
        print("Move");
        rb.velocity= context.ReadValue<Vector2>() * moveSpeed;

        //rb.velocity = moveSpeed;
    }
}
