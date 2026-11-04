using UnityEngine;

public class PlayerController : MonoBehaviour
{
	protected CharacterController characterController { get; private set; }
	protected virtual void Awake()
	{
		characterController=GetComponent<CharacterController>();
	}
}
