using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    
    public bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalInput, verticalInput).normalized;
        // movement = Quaternion.Euler(0, 0, 30f) * movement;

        
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    private void Move()
    {
        rb.velocity = movement * moveSpeed*Time.deltaTime;
        animator.SetFloat("Speed", rb.velocity.magnitude);
        if(movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
