using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float smoothSpeed = 10f;
    
    private float highestY;

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;
        
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
            
            Vector3 targetPosition = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}