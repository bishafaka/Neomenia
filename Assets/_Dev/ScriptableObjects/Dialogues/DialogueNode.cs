using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
	public string text;
	public DialogueNode nextNode;
	public UnityEvent onSelected;
}
[System.Serializable]
public class Dialogue
{
	public string name;
	[TextArea(3, 6)]
	public string text;
	public Sprite sprite;
}
[CreateAssetMenu(menuName="Dialogue/Default")]
public class DialogueNode : ScriptableObject
{
	public List<Dialogue> dialogue=new();
	public List<DialogueChoice> choices=new();
    public void ChangeHeartsEvent(int amount)
    {
        NPCManager.Instance.ChangeHearts(amount);
    }
	public void UpdateMeetEvent()
	{
        NPCManager.Instance.UpdateMeetState();
    }
}
