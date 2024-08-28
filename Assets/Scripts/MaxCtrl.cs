using UnityEngine;
using UnityEngine.InputSystem;

public class MaxCtrl : MonoBehaviour
{
    private new Transform transform;
    private Vector3 moveDir;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.Translate(Vector3.forward * Time.deltaTime * 4f);
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        moveDir = new Vector3(dir.x, 0f, dir.y);
        Debug.Log(dir);
    }
    private void OnAttack()
    {
        Debug.Log("Attack");
    }
}
