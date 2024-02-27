using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BasicJump : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpHeight = 7;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("Jump");
            rb.AddForce(Vector2.up* jumpHeight, ForceMode2D.Impulse);
        }
    }
}
