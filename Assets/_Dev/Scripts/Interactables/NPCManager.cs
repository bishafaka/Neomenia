using UnityEngine;

public class NPCManager : MonoBehaviour
{
	public static NPCManager Instance { get; private set; }
	NPC currentNPC;
	DialogueNode currentDialogue;
	int maxHearts=10;
	int minHearts=-10;
	public NPC GetCurrentNPC()
	{
		return currentNPC;
	}
	void Awake()
	{
		if(Instance==null)
		{
			Instance=this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}
	public DialogueNode SetNPCDialogue(NPC npc, DialogueNode dialogue)
	{
		currentNPC=npc;
		currentDialogue=dialogue;
		UIManager.Instance.dialogueManager.StartDialogue(currentDialogue);
		return dialogue;
	}
	public void ResetNPCDialogue()
	{
		currentNPC=null;
		currentDialogue=null;
	}
	public void ChangeHearts(int amount)
	{
		if(currentNPC==null)
			return;
		currentNPC.hearts=Mathf.Clamp(currentNPC.hearts+amount, minHearts, maxHearts);
	}
	public void UpdateMeetState()
	{
		if(currentNPC!=null)
			currentNPC.haveTheyMetYet=true;
	}
}
