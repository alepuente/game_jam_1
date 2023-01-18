using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    float yaw;
    float pitch;

    void FixedUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * InputController.Instance.mouseSensitivity * Time.fixedDeltaTime;
        pitch -= Input.GetAxis("Mouse Y") * InputController.Instance.mouseSensitivity * Time.fixedDeltaTime;
        pitch = Mathf.Clamp(pitch, InputController.Instance.pitchMinMax.x, InputController.Instance.pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, InputController.Instance.rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * InputController.Instance.distanceFromTarget;
    }
}
