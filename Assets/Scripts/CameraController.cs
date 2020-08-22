using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Attributes")]
    public float PanSpeed = 5f;
    void Update() {

        if (Input.GetKey("w") /*|| Input.mousePosition.y >= Screen.height - BorderThickness*/) {
            transform.Translate(new Vector3(0, 0, 1) * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") /*|| Input.mousePosition.y <= BorderThickness*/) {
            transform.Translate(new Vector3(0, 0, -1) * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") /*|| Input.mousePosition.x <= BorderThickness*/) {
            transform.Translate(new Vector3(-1, 0, 0) * PanSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") /*|| Input.mousePosition.x >= Screen.width - BorderThickness*/) {
            transform.Translate(new Vector3(1, 0, 0) * PanSpeed * Time.deltaTime, Space.World);
        }

    }
}
