using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public GameSettings settings;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.linearVelocity.y <= 0)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, settings.jumpForce);
        }
    }
}
