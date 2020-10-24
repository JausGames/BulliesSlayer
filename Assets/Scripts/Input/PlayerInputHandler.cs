using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerCombat combat;
    [SerializeField] PlayerMovement movement;
    [SerializeField] MouseLook mouse;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        combat = GetComponent<PlayerCombat>();
        movement = GetComponent<PlayerMovement>();
        mouse = GetComponentInChildren<MouseLook>();
    }

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }
    public void OnMove(CallbackContext context)
    {
        if (movement == null) return;
        var move = context.ReadValue<Vector2>();
        movement.SetMovement(move);
    }
    public void OnLook(CallbackContext context)
    {
        if (movement == null) return;
        var look = context.ReadValue<Vector2>();
        mouse.SetLook(look);
    }
    public void OnFire(CallbackContext context)
    {
        Debug.Log("OnAttack");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        combat.Shoot(perf, !canc);
    }
    public void OnAction(CallbackContext context)
    {
        Debug.Log("OnDash");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        combat.Action(perf, canc);
    }
    public void OnReload(CallbackContext context)
    {
        Debug.Log("OnB");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        combat.Reload(perf, canc);
    }
    public void OnThrow(CallbackContext context)
    {
        Debug.Log("OnNorth");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        combat.Throw(perf, canc);
    }
    public void OnJump(CallbackContext context)
    {
        Debug.Log("OnNorth");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        movement.Jump(perf, canc);
    }
    public void OnChangeWeapon(CallbackContext context)
    {
        Debug.Log("OnNorth");
        if (combat == null) return;
        var canc = context.canceled;
        var perf = context.performed;
        combat.ChangeWeapon(perf, canc);
    }
}
