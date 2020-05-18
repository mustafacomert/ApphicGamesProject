using UnityEngine;
using System.Collections;

public class CameraLerpToTransform : MonoBehaviour
{
    public Transform cameraTrackTarget;

    public float trackingSpeed;

    public float cameraZDepth = -10f;

    public float minX;

    public float minY;

    public float maxX;

    public float maxY;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (cameraTrackTarget != null) {
            var newPosition = Vector2.Lerp(transform.position, cameraTrackTarget.position, Time.deltaTime * trackingSpeed);
            var camPosition = new Vector3(newPosition.x, newPosition.y, cameraZDepth);

            var v3 = camPosition;
            var newX = Mathf.Clamp(v3.x, minX, maxX);
            var newY = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = new Vector3(newX, newY, cameraZDepth);
        }
    }
}
