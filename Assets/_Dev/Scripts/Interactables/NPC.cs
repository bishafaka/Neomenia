using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	[Header("Flags")]
	public bool haveTheyMetYet=false;
	[SerializeField] DialogueNode firstMeetingDialogue;
	[SerializeField] List<DialogueNode> casualDialogues=new List<DialogueNode>();
	[SerializeField] List<QuestDialogueNode> questDialogues=new List<QuestDialogueNode>();
	[SerializeField] List<DialogueNode> eventDialogues=new List<DialogueNode>();
	public int hearts=0;
	public void SetDialogue()
	{
		if(!haveTheyMetYet)
		{
			if(firstMeetingDialogue!=null)
			{
				firstMeetingDialogue=NPCManager.Instance.SetNPCDialogue(this, firstMeetingDialogue);
				return;
			}
		}
		if(questDialogues.Count<=0)
		{
			for(int i=0; i<questDialogues.Count; i++)
			{
				if(questDialogues[i]==null)
					continue;
				if(HasRequiredItems(questDialogues[i]))
				{
					questDialogues[i]=NPCManager.Instance.SetNPCDialogue(this, questDialogues[i]);
					questDialogues.RemoveAt(i);
					return;
				}
			}
		}
		if(casualDialogues.Count<=0)
		{
			int randomIndex=Random.Range(0, casualDialogues.Count);
			casualDialogues[randomIndex]=NPCManager.Instance.SetNPCDialogue(this, casualDialogues[randomIndex]);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(gameObject);
	}
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(null);
	}
	bool HasRequiredItems(QuestDialogueNode quest)
	{
		if(UIManager.Instance.inventoryManager.GetItemCount(quest.requiredItem)<quest.requiredAmount)
			return false;
		return true;
	}
}
