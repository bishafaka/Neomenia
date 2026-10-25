using UnityEngine;

public class PlayerInteract : MonoBehaviour
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
			interactableInside.GetComponent<NPC>().SetDialogue();
		}
	}
	public void SetInteractable(GameObject interactable) => interactableInside=interactable;
}
