#pragma warning disable IDE0051, CS0168, CS0219
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    private Animator ani;
    private new Transform transform;
    private Vector3 moveDir;
    //----------------------------------------------
    // C# 이벤트 방법
    public PlayerInput playerInput;
    public InputActionMap mainActionMap;
    public InputAction moveAction;
    public InputAction attackAction;

    void Start()
    {
        ani = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();

        mainActionMap = playerInput.actions.FindActionMap("PlayerActions");
        moveAction = mainActionMap.FindAction("Move");
        attackAction = mainActionMap.FindAction("Attack");

        moveAction.performed += context =>
        {
            Vector2 dir = context.ReadValue<Vector2>();
            moveDir = new Vector3(dir.x, 0f, dir.y);
            ani.SetFloat("Movement", dir.magnitude);
        };
        moveAction.canceled += context =>
        {
            moveDir = Vector3.zero;
            ani.SetFloat("Movement", 0.0f);
        };

        attackAction.performed += context =>
        {
            ani.SetTrigger("Attack");
        };
    }

    void Update()
    {
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4f);
        }
    }

    #region SendMessage 방법
    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        ani.SetFloat("Movement", dir.magnitude);
        Debug.Log("SendMessage : " + dir);
    }

    private void OnAttack()
    {
        ani.SetTrigger("Attack");
        Debug.Log("SendMessage : Attack");
    }
    #endregion

    #region Unity_Invoke 방법

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        ani.SetFloat("Movement", dir.magnitude);
        Debug.Log("Unity_Invoke : " + dir);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.performed)
        {
            ani.SetTrigger("Attack");
            Debug.Log("Unity_Invoke : Attack");
        }
    }

    #endregion

}