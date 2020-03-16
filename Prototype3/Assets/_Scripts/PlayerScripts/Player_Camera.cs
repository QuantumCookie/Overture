using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //Player Camera variables
    public float cameraHeight = 20f;
    public Vector2 cameraDistance = new Vector2(0, 2f);
    public Camera playerCamera;

    private Vector3 cameraSmoothVelocity;

    public float cameraSmoothTime = 0.3f;
    public float playerRotationSmoothing = 0.5f;
    public Vector2 mouseLookSmoothing;

    //Mouse cursor Camera offset effect
    Vector2 playerPosOnScreen;
    Vector2 cursorPosition;
    Vector2 cursorOffsetVector;

    void FixedUpdate()
    {
        /*//Setup camera offset
        Vector3 cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        cursorOffsetVector = cursorPosition - playerPosOnScreen;

        Vector2 mouseOffset = new Vector2(cursorOffsetVector.x * mouseLookSmoothing.x, cursorOffsetVector.y * mouseLookSmoothing.y);
        Vector3 netOffset = new Vector3(mouseOffset.x, (Mathf.Abs(mouseOffset.x) + Mathf.Abs(mouseOffset.y)), mouseOffset.y);

        cameraOffset += new Vector3(0, netOffset.y, 0);

        //Camera Follow
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, transform.position + cameraOffset, ref cameraSmoothVelocity, cameraSmoothTime);
        playerCamera.transform.rotation = Quaternion.LookRotation(transform.position + new Vector3(netOffset.x, 0, netOffset.z) - playerCamera.transform.position);*/

        //Setup camera offset
        Vector3 cameraOffset = new Vector3(-cameraDistance.x, cameraHeight, -cameraDistance.y);

        //Mouse cursor offset effect
        playerPosOnScreen = playerCamera.WorldToViewportPoint(transform.position);
        cursorPosition = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        cursorOffsetVector = cursorPosition - playerPosOnScreen;

        Vector2 mouseOffset = new Vector2(cursorOffsetVector.x * mouseLookSmoothing.x, cursorOffsetVector.y * mouseLookSmoothing.y);

        cameraOffset += new Vector3(mouseOffset.x, (Mathf.Abs(mouseOffset.x) + Mathf.Abs(mouseOffset.y)) * 0.5f, mouseOffset.y);

        //Camera Follow
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, transform.position + cameraOffset, ref cameraSmoothVelocity, cameraSmoothTime);
    }
}