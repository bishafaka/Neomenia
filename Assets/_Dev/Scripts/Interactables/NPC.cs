using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	[Header("Stats")]
	public bool haveTheyMetYet=false;
	public int hearts=0;
	[Header("Dialogues")]
	[SerializeField] DialogueNode firstMeetingDialogue;
	[SerializeField] List<DialogueNode> casualDialogues=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> questDialogues=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> eventDialogues=new List<DialogueNode>();
	public void SetDialogue()
	{
		//FIRST MEETING
		if(!haveTheyMetYet && firstMeetingDialogue!=null)
		{
			NPCManager.Instance.SetNPCDialogue(this, firstMeetingDialogue);
			return;
		}
		//QUESTS
		for(int i=0; i<questDialogues.Count; i++)
		{
			if(questDialogues[i]==null)
				continue;
			NPCManager.Instance.SetNPCDialogue(this, questDialogues[i]);
			return;
		}
		//EVENTS
		for(int i=0; i<eventDialogues.Count; i++)
		{
			if(eventDialogues[i]==null)
				continue;
			NPCManager.Instance.SetNPCDialogue(this, eventDialogues[i]);
			return;
		}
		//CASUAL
		if(casualDialogues.Count>0)
		{
			int randomIndex=Random.Range(0, casualDialogues.Count);
			NPCManager.Instance.SetNPCDialogue(this, casualDialogues[randomIndex]);
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
}
