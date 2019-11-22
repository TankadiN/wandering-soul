using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement inst;
    public static Vector2 Position;

    public float moveSpeed = 5f;
    public float moveSmooth = .3f;
    public bool aimAtCursor;

    private Rigidbody2D rb;
    private Animator anim;


    Vector2 movement = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    Vector2 mousePos = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("moveX", rb.velocity.x);
        anim.SetFloat("moveY", rb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("lastmoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("lastmoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    private void FixedUpdate()
    {

        Vector2 desiredVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, desiredVelocity, ref velocity, moveSmooth);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if(aimAtCursor)
        {
            rb.rotation = angle + 180f;
        }

        Position = rb.position;
    }


    public void SwitchEnabled()
    {
        if(enabled)
        {
            enabled = false;
        }
        else
        {
            enabled = true;
        }
    }
}
