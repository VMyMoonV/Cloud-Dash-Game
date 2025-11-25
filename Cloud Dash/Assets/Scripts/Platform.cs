using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 15f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (playerRb != null && playerRb.linearVelocity.y <= 0)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
                Debug.Log($"Player jumped! Force: {jumpForce}");
            }
        }
    }
}