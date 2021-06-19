using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "inputReader", menuName = "ScriptableObjects/Gameplay/InputReader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions
{
    //Gameplay
    public event UnityAction BoostEvent;
    public event UnityAction<Vector2> ChargeEvent;
    public event UnityAction LetGoEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.gameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    //will send aimDirection when using
    public void OnCharge(InputAction.CallbackContext ctx)
    {
        //you might only need the canceled part, test it out mate
        if (ctx.phase == InputActionPhase.Performed)
        {
            ChargeEvent?.Invoke(ctx.ReadValue<Vector2>());
        }
    }

    public void OnLetGo(InputAction.CallbackContext ctx)
    {
        //will sling away the snus when you let go of the aim button
        if (ctx.phase == InputActionPhase.Canceled)
        {
            LetGoEvent?.Invoke();
        }
    }

    //will boost the snus a little bit in air when tapepd
    public void OnBoost(InputAction.CallbackContext ctx)
    {
        //will attack when you let go of the aim button
        if (ctx.phase == InputActionPhase.Canceled)
        {
            BoostEvent?.Invoke();
        }
    }
    
    public void EnableGameplayInput()
    {
        _controls.gameplay.Enable();
    }

    public void DisableAllInput()
    {
        _controls.gameplay.Disable();
    }
}
