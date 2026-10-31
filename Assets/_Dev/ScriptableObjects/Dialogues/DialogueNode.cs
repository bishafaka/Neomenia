using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public enum ConditionType{HasItem, DoesNotHaveItem, HeartsGreaterThan, HeartsLessThan, HeartsEquals, BoolVariable, IntVariable, QuestCompleted, QuestNotCompleted}
[System.Serializable]
public class DialogueCondition
{
	public ConditionType type;
	[Header("Item")]
	public Item item;
	public int amount=1;
	[Header("Hearts or Int")]
	public int value;
	[Header("Bool, Int or Quest")]
	public string variableName;
	public bool boolValue=true;
	public bool Evaluate(NPC npc)
	{
		switch(type)
		{
			case ConditionType.HasItem:
				return UIManager.Instance.inventoryManager.GetItemCount(item)>=amount;
			case ConditionType.DoesNotHaveItem:
				return UIManager.Instance.inventoryManager.GetItemCount(item)<amount;
			case ConditionType.HeartsGreaterThan:
				return npc!=null && npc.hearts>value;
			case ConditionType.HeartsLessThan:
				return npc!=null && npc.hearts<value;
			case ConditionType.HeartsEquals:
				return npc!=null && npc.hearts==value;
			case ConditionType.BoolVariable:
				return GameVariables.Instance.GetBool(variableName)==boolValue;
			case ConditionType.IntVariable:
				return GameVariables.Instance.GetInt(variableName)==value;
			case ConditionType.QuestCompleted:
				return GameVariables.Instance.GetBool(variableName);
			case ConditionType.QuestNotCompleted:
				return !GameVariables.Instance.GetBool(variableName);
		}
		return true;
	}
}
[System.Serializable]
public class DialogueChoice
{
	public string text;
	public DialogueNode nextNode;
	[Header("Conditions")]
	public bool requiresConditions;
	public bool showIfUnavailable;
	public string unavailableText;
	public ConditionMode conditionMode=ConditionMode.All;
	public DialogueCondition[] conditions;
	[Header("Events")]
	public UnityEvent onSelected;
	public bool CanSelect(NPC npc)
	{
		if(!requiresConditions || conditions==null || conditions.Length==0)
			return true;
		if(conditionMode==ConditionMode.All)
		{
			foreach(DialogueCondition condition in conditions)
			{
				if(!condition.Evaluate(npc))
					return false;
			}
			return true;
		}
		foreach(DialogueCondition condition in conditions)
		{
			if(condition.Evaluate(npc))
				return true;
		}
		return false;
	}
	public string GetDisplayText(NPC npc)
	{
		if(!CanSelect(npc))
			return GameVariables.Instance.ReplaceVariables(unavailableText);
		return GameVariables.Instance.ReplaceVariables(text);
	}
}
public enum ConditionMode{All, Any}
[System.Serializable]
public class Dialogue
{
	public string name;
	[TextArea(3, 6)]
	public string text;
	public Sprite sprite;
}
[CreateAssetMenu(menuName="Dialogue")]
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
	public void SetBoolEvent(string variable)
	{
		GameVariables.Instance.SetBool(variable, true);
	}
	public void AddIntEvent(string variable)
	{
		GameVariables.Instance.AddInt(variable, 1);
	}
}
