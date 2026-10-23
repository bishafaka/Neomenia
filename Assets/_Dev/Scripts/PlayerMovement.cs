using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed=5f;
    Vector2 input;
    void OnEnable()
    {
        if(InputManager.Instance!=null)
            InputManager.Instance.Move+=OnMove;
    }
    void OnDisable()
    {
        if(InputManager.Instance!=null)
            InputManager.Instance.Move-=OnMove;
    }
    void FixedUpdate()
    {
        Vector3 movement=new Vector3(input.x, 0, input.y).normalized;
        transform.position+=movement*speed*Time.fixedDeltaTime;
    }
    void OnMove(Vector2 dir) => input=dir;
}
