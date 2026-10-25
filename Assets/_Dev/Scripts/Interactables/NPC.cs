using UnityEngine;

public class NPC : MonoBehaviour
{
	[SerializeField] DialogueNode dialogue;
	public void SetDialogue()
	{
		UIManager.Instance.dialogueManager.StartDialogue(dialogue);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(gameObject);
	}
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(null);
	}
}
