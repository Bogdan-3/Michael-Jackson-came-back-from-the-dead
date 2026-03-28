using UnityEngine;
using UnityEngine.InputSystem;

public class rotation : MonoBehaviour
{
    Player inputActions;
    float RotateX,RotateY;

     void Awake()
    {
        RotateX=0;
        RotateY=0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputActions = new Player();
        inputActions.Camera.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        RotateX += inputActions.Camera.yaw.ReadValue<float>();
        RotateY -= inputActions.Camera.pitch.ReadValue<float>();
        RotateY = Mathf.Clamp(RotateY, -12f, 89f);
        transform.rotation = Quaternion.Euler(RotateY, RotateX, 0);
    }
}
