using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float zoomSpeed = 100.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 100.0f;

    void Update() {
        // Get the Mousewheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Scroll: " + scroll);

        // Calculate the new camera size (orthographic size for 2D)
        float newSize = Camera.main.orthographicSize - scroll * zoomSpeed;

        // Clamp the size within min and max values
        // newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        // Apply the new size to the camera
        Camera.main.orthographicSize = newSize;
        Debug.Log("NewSize: " + Camera.main.orthographicSize);
    }
}