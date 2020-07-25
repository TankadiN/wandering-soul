using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    private Vector3 StartPos = Vector3.zero;

    public float moveSpeed = 5f;
    public float Sensitivity;
    public float moveSmooth = .3f;
    public bool aimAtCursor;

    private Vector2 Position;

    public int MinX, MaxX;
    public int MinY, MaxY;

    private Rigidbody2D rb;

    Vector2 movement = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    Vector2 mousePos = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetHeart();
    }

    public void SetHeart()
    {
        transform.position = StartPos;
        Position = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //Vector2 desiredVelocity = movement * moveSpeed;
        //rb.velocity = Vector2.SmoothDamp(rb.velocity, desiredVelocity, ref velocity, moveSmooth);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (aimAtCursor)
        {
            rb.rotation = angle + 180f;
        }

        float horizontal = Input.GetAxis("Horizontal") * Sensitivity;
        float vertical = Input.GetAxis("Vertical") * Sensitivity;

        Position.x += horizontal;
        Position.y += vertical;

        Position.x = Mathf.Clamp(Position.x, MinX, MaxX);
        Position.y = Mathf.Clamp(Position.y, MinY, MaxY);

        transform.position = Vector2.Lerp(transform.position, Position, moveSpeed);
    }
}
