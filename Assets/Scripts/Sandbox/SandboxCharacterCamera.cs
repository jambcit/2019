using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxCharacterCamera
{
    Camera myCamera;
    Transform targetCharacter;

    public float mouseX;
    public float mouseY;

    public float cameraDistance;

    public SandboxCharacterCamera(Camera myCamera, Transform targetCharacter, float cameraDistance = 10)
    {
        this.myCamera = myCamera;
        this.targetCharacter = targetCharacter;
        this.cameraDistance = cameraDistance;
    }

    public void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -60, 60);
    }

    public void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector3 cameraStart = new Vector3(0, 0, -cameraDistance);
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);

        myCamera.transform.position = targetCharacter.position + rotation * cameraStart;
        myCamera.transform.LookAt(targetCharacter);
    }

}


