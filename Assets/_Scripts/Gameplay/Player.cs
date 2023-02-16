using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform gfx;

    public float speed;
    public float jumpForce;
    public float maxJumpTime;
    public float coyoteTime;
    private float jumpTimer;
    private float movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (movement < 0 && gfx.rotation.y < 180)
        {
            gfx.DORotate(new Vector3(0, 180, 0), .2f);
        } else if (movement > 0 && gfx.rotation.y > 0)
        {
            gfx.DORotate(Vector3.zero, .2f);
        }

        if (Input.GetButton("Jump") && jumpTimer < maxJumpTime)
        {
            jumpTimer += Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * speed * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpTimer = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(CoyoteTime());
    }

    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        jumpTimer = maxJumpTime;
    }
}