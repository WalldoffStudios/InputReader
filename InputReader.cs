using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "inputReader", menuName = "ScriptableObjects/Gameplay/InputReader")]
public class InputReader : ScriptableObject, Controls.IGameplayActions {
    
    //Gameplay events, all input events you use should be listed here
    //subscribe to these from scripts that will be affected by your input
    #region Events

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<bool> StopEvent;
    public event UnityAction<Vector2> AimEvent;
    public event UnityAction<bool> ShootingEvent;
    public event UnityAction InteractEvent;

    #endregion Events


    //inputActions reference
    private Controls _controls;
    
    #region Callback functions

    private void OnEnable() {
        if (_controls == null) {
            _controls = new Controls();
            _controls.Gameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable() {
        DisableAllInput();
    }
    
    #endregion Callback functions

    #region Enable/Disable input
    public void EnableGameplayInput() => _controls.Gameplay.Enable();
    public void DisableAllInput() => _controls.Gameplay.Disable();
    
    #endregion Enable/Disable input

    //Put all of your input action events inside of this region
    #region Input callbacks

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) {
            StopEvent?.Invoke(false);
        }
        if (ctx.phase == InputActionPhase.Performed) {
            MoveEvent?.Invoke(ctx.ReadValue<Vector2>());
        }
        if (ctx.phase == InputActionPhase.Canceled) {
            StopEvent?.Invoke(true);
        }
    }

    public void OnAiming(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed) {
            AimEvent?.Invoke(ctx.ReadValue<Vector2>());
        }
    }

    public void OnInteracting(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            InteractEvent?.Invoke();
        }
    }
    
    #endregion Input callbacks

    //This can be removed if you wont be using my custom joystick
    //Needed to create a custom joystick for this callback to function properly
    #region Custom Input Callbacks

    //Used to keep track of player holding down trigger
    public void OnShoot(bool shoot) => ShootingEvent?.Invoke(shoot);

    #endregion Custom Input Callback
}
