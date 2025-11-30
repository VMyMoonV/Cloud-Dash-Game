using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameSettings settings;
    public Transform player;

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
            Vector3 target = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, settings.cameraSmoothSpeed * Time.deltaTime);
        }
    }
}
