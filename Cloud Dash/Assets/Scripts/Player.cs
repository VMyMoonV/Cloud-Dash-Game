using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0)
            sprite.flipX = false;
        else if (horizontalInput < 0)
            sprite.flipX = true;
        
        WrapScreen();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void WrapScreen()
    {
        if (transform.position.x > 2.5f)
            transform.position = new Vector2(-2.5f, transform.position.y);
        
        if (transform.position.x < -2.5f)
            transform.position = new Vector2(2.5f, transform.position.y);
    }
}