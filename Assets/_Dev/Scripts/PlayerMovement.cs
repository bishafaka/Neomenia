using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float speed=5f;
	[SerializeField] float sprintMultiplier=1.5f;
	Vector2 input;
	bool isSprint=false;
	CharacterController characterController;
    void Awake()
    {
        characterController=GetComponent<CharacterController>();
    }
    void OnEnable()
	{
		if(InputManager.Instance!=null)
		{
			InputManager.Instance.Move+=OnMove;
			InputManager.Instance.SprintPressed+=OnSprintPressed;
			InputManager.Instance.SprintReleased+=OnSprintReleased;
		}
	}
	void OnDisable()
	{
		if(InputManager.Instance!=null)
		{
			InputManager.Instance.Move-=OnMove;
			InputManager.Instance.SprintPressed-=OnSprintPressed;
			InputManager.Instance.SprintReleased-=OnSprintReleased;
		}
	}
	void Update()
	{
		Vector3 movement=new Vector3(input.x, 0, input.y).normalized;
		float currentSpeed=isSprint ? speed*sprintMultiplier : speed;
		characterController.Move(movement*currentSpeed*Time.deltaTime);
	}
	void OnMove(Vector2 dir) => input=dir;
	void OnSprintPressed() => isSprint=true;
	void OnSprintReleased() => isSprint=false;
}
