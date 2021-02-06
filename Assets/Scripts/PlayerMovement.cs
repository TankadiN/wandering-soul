using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement inst;
    public static Vector2 Position;

    public bool isBattle;

    public Vector2 StartPos;

    public float moveSpeed = 5f;
    public float moveSmooth = .3f;
    public bool aimAtCursor;
    [HideInInspector]
    public Rigidbody2D rb;
    private Animator anim;
    private Interaction inter;
    [HideInInspector]
    public Vector2 movement = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    Vector2 mousePos = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inter = GetComponent<Interaction>();
        inst = this;
        if(isBattle)
        {
            SetHeart();
        }
    }

    public void SetHeart()
    {
        transform.position = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (!isBattle)
        {
            anim.SetFloat("moveX", rb.velocity.x);
            anim.SetFloat("moveY", rb.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (!isBattle)
            {
                anim.SetFloat("lastmoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("lastmoveY", Input.GetAxisRaw("Vertical"));
            }
        }
    }

    private void FixedUpdate()
    {

        Vector2 desiredVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, desiredVelocity, ref velocity, moveSmooth);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (aimAtCursor)
        {
            rb.rotation = angle + 180f;
        }

        Position = rb.position;
    }
}
