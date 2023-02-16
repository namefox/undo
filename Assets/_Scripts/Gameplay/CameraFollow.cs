using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed;

    private void FixedUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
    }
}