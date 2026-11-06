using UnityEngine;

public class PlayerInteract : PlayerController
{
	GameObject interactableInside=null;
	void OnEnable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.InteractPressed+=Interact;
	}
	void OnDisable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.InteractPressed-=Interact;
	}
	void Interact()
	{
		if(interactableInside==null)
			return;
		if(interactableInside.CompareTag("NPC"))
		{
			InputManager.Instance.SetPlayerStateUI();
			interactableInside.GetComponent<NPC>().NPCGreet();
		}
		if(interactableInside.CompareTag("Building"))
		{
			characterController.enabled=false;
			Vector3 targetPosition=interactableInside.GetComponent<Building>().entranceTransform.position;
			gameObject.transform.position=targetPosition;
			characterController.enabled=true;
		}
	}
	public void SetInteractable(GameObject interactable) => interactableInside=interactable;
}
