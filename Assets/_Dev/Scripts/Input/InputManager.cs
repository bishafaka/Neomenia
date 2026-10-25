using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-20)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    InputSystemActions Inputs;

    public enum PlayerState
    {
        UI,
        Player,
        Cinematic
    }
    public PlayerState Player=PlayerState.UI;

    public event Action<Vector2> Move;
    public event Action<Vector2> Look;
    public event Action JumpPressed;
    public event Action JumpReleased;
    public event Action InteractPressed;
    public event Action InteractReleased;
    public event Action SprintPressed;
    public event Action SprintReleased;
    public event Action PausePressed;
    public event Action InventoryPressed;

    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
            DontDestroyOnLoad(this);
            Inputs=new InputSystemActions();
            RegisterEvent();
            CheckState();
        }
        else
            Destroy(gameObject);
    }
    void OnDestroy()
    {
        UnregisterEvent();
        Inputs.Disable();
    }
    void RegisterEvent()
    {
        Inputs.Player.Move.performed+=OnMove;
        Inputs.Player.Move.canceled+=OnMove;
        Inputs.Player.Look.performed+=OnLook;
        Inputs.Player.Look.canceled+=OnLook;
        Inputs.Player.Jump.started+=OnJumpStarted;
        Inputs.Player.Jump.canceled+=OnJumpCanceled;
        Inputs.Player.Interact.started+=OnInteractStarted;
        Inputs.Player.Interact.canceled+=OnInteractCanceled;
        Inputs.Player.Sprint.started+=OnSprintStarted;
        Inputs.Player.Sprint.canceled+=OnSprintCanceled;
        Inputs.Default.Pause.started+=OnPauseStarted;
        Inputs.Default.Inventory.started+=OnInventoryStarted;
    }
    void UnregisterEvent()
    {
        Inputs.Player.Move.performed-=OnMove;
        Inputs.Player.Move.canceled-=OnMove;
        Inputs.Player.Look.performed-=OnLook;
        Inputs.Player.Look.canceled-=OnLook;
        Inputs.Player.Jump.started-=OnJumpStarted;
        Inputs.Player.Jump.canceled-=OnJumpCanceled;
        Inputs.Player.Interact.started-=OnInteractStarted;
        Inputs.Player.Interact.canceled-=OnInteractCanceled;
        Inputs.Player.Sprint.started-=OnSprintStarted;
        Inputs.Player.Sprint.canceled-=OnSprintCanceled;
        Inputs.Default.Pause.started-=OnPauseStarted;
        Inputs.Default.Inventory.started-=OnInventoryStarted;
    }
    void OnMove(InputAction.CallbackContext ctx) => Move?.Invoke(ctx.ReadValue<Vector2>());
    void OnLook(InputAction.CallbackContext ctx) => Look?.Invoke(ctx.ReadValue<Vector2>());
    void OnJumpStarted(InputAction.CallbackContext ctx) => JumpPressed?.Invoke();
    void OnJumpCanceled(InputAction.CallbackContext ctx) => JumpReleased?.Invoke();
    void OnInteractStarted(InputAction.CallbackContext ctx) => InteractPressed?.Invoke();
    void OnInteractCanceled(InputAction.CallbackContext ctx) => InteractReleased?.Invoke();
    void OnSprintStarted(InputAction.CallbackContext ctx) => SprintPressed?.Invoke();
    void OnSprintCanceled(InputAction.CallbackContext ctx) => SprintReleased?.Invoke();
    void OnPauseStarted(InputAction.CallbackContext ctx) => PausePressed?.Invoke();
    void OnInventoryStarted(InputAction.CallbackContext ctx) => InventoryPressed?.Invoke();
    public void SetPlayerStatePlayer()
    {
        Player=PlayerState.Player;
        CheckState();
    }
    public void SetPlayerStateUI()
    {
        Player=PlayerState.UI;
        CheckState();
    }
    public void SetPlayerStateCinematic()
    {
        Player=PlayerState.Cinematic;
        CheckState();
    }
    void CheckState()
    {
        Inputs.Default.Enable();
        switch(Player)
        {
            case PlayerState.UI:
                SetUI();
                break;
            case PlayerState.Player:
                SetPlayer();
                break;
            case PlayerState.Cinematic:
                SetCinematic();
                break;
            default:
                break;
        }
    }
    void SetUI()
    {
        Inputs.UI.Enable();
        Inputs.Player.Disable();
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.None;
    }
    void SetPlayer()
    {
        Inputs.Player.Enable();
        Inputs.UI.Disable();
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.None;
    }
    void SetCinematic()
    {
        Inputs.Player.Disable();
        Inputs.UI.Disable();
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.None;
    }
}
