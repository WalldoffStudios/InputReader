using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "inputReader", menuName = "ScriptableObjects/Gameplay/InputReader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions {
    
    //Gameplay events, all input events you use should be listed here
    //subscribe to these from scripts that will be affected by your input
    #region Events

    //WASD || Joysticks
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<bool> StopEvent;
    public event UnityAction<Vector2> LookEvent;
    
    //Space ||  button south
    public event UnityAction JumpEvent;
    
    //Right MB || left trigger
    public event UnityAction<bool> AimEvent;

    //Left MB || right trigger
    public event UnityAction<bool> ShootEvent;

    #endregion Events


    //inputActions reference
    private Controls _controls;
    
    #region Callback functions

    private void OnEnable() 
    {
        if (_controls == null) 
        {
            _controls = new Controls();
            _controls.Gameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable() 
    {
        DisableAllInput();
    }
    
    #endregion Callback functions

    #region Enable/Disable input
    private void EnableGameplayInput() => _controls.Gameplay.Enable();
    private void DisableAllInput() => _controls.Gameplay.Disable();
    
    #endregion Enable/Disable input

    //Put all of your input action events inside of this region
    #region Input callbacks

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) 
        {
            StopEvent?.Invoke(false);
        }
        if (ctx.phase == InputActionPhase.Performed) 
        {
            MoveEvent?.Invoke(ctx.ReadValue<Vector2>());
        }
        if (ctx.phase == InputActionPhase.Canceled) 
        {
            StopEvent?.Invoke(true);
        }
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed) 
        {
            LookEvent?.Invoke(ctx.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            AimEvent?.Invoke(true);
        }
        if (ctx.phase == InputActionPhase.Canceled)
        {
            AimEvent?.Invoke(false);
        }
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            ShootEvent?.Invoke(true);
        }
        if (ctx.phase == InputActionPhase.Canceled)
        {
            ShootEvent?.Invoke(false);
        }
    }

    #endregion Input callbacks
}
