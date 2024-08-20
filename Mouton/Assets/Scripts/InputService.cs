using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService {
    private Controls controls;
    private Camera _mainCam;
    public event Action Jumped;
    public event Action PickedUp;
    public event Action LeftDown;
    public event Action Interacted;
    public event Action RightClick;
    public event Action Paused;
    public event Action Resumed;
    public bool Activated {
        get => controls.Player.enabled;
        set {
            if(value) {
                controls.Player.Enable();
                controls.Pause.Disable();
            }
            else {
                controls.Player.Disable();
                controls.Pause.Enable();
            }
        }
    }
    public InputService() {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Pickup.performed += HandlePickup;
        controls.Player.LeftClick.performed += HandleLeftDown;
        controls.Player.Interact.performed += HandleInteract;
        controls.Player.RightClick.performed += HandleRightDown;
        controls.Player.Pause.performed += HandlePause;
        controls.Pause.Resume.performed += HandleResume;
    }

    private void HandleResume(InputAction.CallbackContext context)
    {
        Resumed?.Invoke();
    }

    private void HandlePause(InputAction.CallbackContext context)
    {
        Paused?.Invoke();
    }

    private void HandleRightDown(InputAction.CallbackContext context)
    {
        RightClick?.Invoke();
    }

    public void HandleInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        Interacted?.Invoke();       
    }
    public void HandleJump(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        Jumped?.Invoke();       
    }
    public void HandlePickup(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        PickedUp?.Invoke();
    }
    public void HandleLeftDown(UnityEngine.InputSystem.InputAction.CallbackContext callback) {
        LeftDown?.Invoke();  
    }
    public Camera MainCam => _mainCam ? _mainCam : _mainCam = Camera.main;   
    public Vector2 ScreenMouse => controls.Player.MousePosition.ReadValue<Vector2>();
    public Vector2 WorldMouse => MainCam.ScreenToWorldPoint(ScreenMouse);
    public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
}