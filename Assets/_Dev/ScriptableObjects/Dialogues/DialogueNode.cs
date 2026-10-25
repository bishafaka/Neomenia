using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueChoice
{
	public string text;
	public DialogueNode nextNode;
}

[System.Serializable]
public class Dialogue
{
	public string name;
	[TextArea(3, 6)]
	public string text;
	public Sprite sprite;
}

[CreateAssetMenu(menuName="Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
	public List<Dialogue> dialogue=new();
	public List<DialogueChoice> choices=new();
}
