using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
	[Header("Stats")]
	public bool haveTheyMetYet=false;
	public bool haveGreetToday=false;
	public int hearts=0;
	[Header("Dialogues")]
	[SerializeField] DialogueNode firstMeetingDialogue;
	[SerializeField] List<DialogueNode> greetDialogue=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> followUpDialogues=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> casualDialogues=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> questDialogues=new List<DialogueNode>();
	[SerializeField] List<DialogueNode> eventDialogues=new List<DialogueNode>();
	public void NPCGreet()
	{
		//FIRST MEETING
		if(!haveTheyMetYet && firstMeetingDialogue!=null)
		{
			NPCManager.Instance.SetNPCDialogue(this, firstMeetingDialogue);
			return;
		}
		//ALREADY GREET
		if(haveGreetToday && followUpDialogues.Count>0)
		{
			int randomIndex=Random.Range(0, followUpDialogues.Count);
			NPCManager.Instance.SetNPCDialogue(this, followUpDialogues[randomIndex]);
			return;
		}
		//GREET
		if(greetDialogue.Count>0)
		{
			int randomIndex=Random.Range(0, greetDialogue.Count);
			NPCManager.Instance.SetNPCDialogue(this, greetDialogue[randomIndex]);
			haveGreetToday=true;
			return;
		}
	}
	public void SetDialogue()
	{
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
			return;
		}
	}
}
