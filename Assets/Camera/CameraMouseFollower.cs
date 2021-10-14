using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollower : MonoBehaviour
{
    private Camera mycam;
    private Vector3 camRotation;
    private Transform cam;

    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;

    // Start is called before the first frame update
    void Start()
    {
        mycam = GetComponent<Camera>();
        cam = mycam.transform;
        Debug.Log("camera : " + mycam.name);
    }

    // Update is called once per frame
    void Update()
    {

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.y += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }
}
