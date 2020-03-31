using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform mainCamera;
    public float speed = 10;

    //Player Camera variables
    public float cameraHeight = 20f;
    public Vector2 cameraDistance = new Vector2(0, 10f);
    public Camera playerCamera;

    private Vector3 cameraSmoothVelocity;

    public float cameraSmoothTime = 0.5f;
    public float playerRotationSmoothing = 0.5f;
    public Vector2 mouseLookSmoothing;

    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 offsetVector;

    Vector3 cameraOffset;

    public Transform debugCube;

    private void Start() 
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        //Setup camera offset
        cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        offsetVector = cursorPosition - playerPosOnScreen;

        Vector2 mouseOffset = new Vector2(offsetVector.x * mouseLookSmoothing.x, offsetVector.y * mouseLookSmoothing.y);

        cameraOffset += new Vector3(0, (Mathf.Abs(mouseOffset.x) + Mathf.Abs(mouseOffset.y)), 0);

        Vector3 screenCenter = playerCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) * 0.5f);
        screenCenter.y = 0;

        //Camera Follow
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, transform.position + cameraOffset, ref cameraSmoothVelocity, cameraSmoothTime);
        playerCamera.transform.LookAt(transform.position + new Vector3(2 * mouseOffset.x, 0, 2 * mouseOffset.y));

        debugCube.transform.position = transform.position + new Vector3(2 * mouseOffset.x, 0, 2 * mouseOffset.y);
    }
}
