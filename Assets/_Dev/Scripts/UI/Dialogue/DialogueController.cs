using UnityEngine;

public class DialogueController : MonoBehaviour
{
	void OnEnable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.DialoguePressed+=UIManager.Instance.dialogueManager.NextDialogue;
	}
	void OnDisable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.DialoguePressed-=UIManager.Instance.dialogueManager.NextDialogue;
	}
}
