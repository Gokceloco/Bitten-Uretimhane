using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;

    public float smoothTime;

    private Vector3 _velocity;

    private void FixedUpdate()
    {
        var targetPos = followObject.transform.position;
        targetPos.y = 0;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}
