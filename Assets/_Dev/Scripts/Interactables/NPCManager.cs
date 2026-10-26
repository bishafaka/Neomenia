using UnityEngine;

public class NPCManager : MonoBehaviour
{
	public static NPCManager Instance { get; private set; }
	NPC currentNPC;
	DialogueNode currentDialogue;
	int maxHearts=10;
	int minHearts=-10;
	void Awake()
	{
		if (Instance==null)
		{
			Instance=this;
			DontDestroyOnLoad(this);
		}
		else
			Destroy(gameObject);
	}
	public DialogueNode SetNPCDialogue(NPC npc, DialogueNode dialogue)
	{
		currentNPC=npc;
		currentDialogue=dialogue;
		UIManager.Instance.dialogueManager.StartDialogue(currentDialogue);
		return null;
	}
    public QuestDialogueNode SetNPCDialogue(NPC npc, QuestDialogueNode dialogue)
    {
        currentNPC=npc;
        currentDialogue=dialogue;
        UIManager.Instance.dialogueManager.StartDialogue(currentDialogue);
        return null;
    }
    public void	ResetNPCDialogue()
	{
		currentNPC=null;
		currentDialogue=null;
	}
	public void ChangeHearts(int amount)
	{
		currentNPC.hearts=Mathf.Clamp(currentNPC.hearts+amount, minHearts, maxHearts);
	}
	public void UpdateMeetState()
	{
        currentNPC.haveTheyMetYet=true;
    }
}
