using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    [SerializeField] private bool IsVisible;
    //[SerializeField] private float _delayTime = 0.5f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        if (!IsVisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (IsVisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 40f);

        // rotate cam and orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //Invoke("Centering", _delayTime);
    }

    private void Centering()
    {
        if (IsVisible)
        {
            var center = Screen.safeArea.center;
            if (new Vector2(Input.mousePosition.x, Input.mousePosition.y) != center)
            {
                Mouse.current.WarpCursorPosition(center);
                InputState.Change(Mouse.current.position, center);
            }
        }
    }
    
    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}