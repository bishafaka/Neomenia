using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public DialogueNode currentDialogue;
	[Header("References")]
	[SerializeField] TextMeshProUGUI nameText;
	[SerializeField] TextMeshProUGUI dialogueText;
	[SerializeField] Image portraitImage;
	[SerializeField] GameObject[] choices;
	int dialogueIndex;
	void Awake()
	{
		HideChoices();
	}
	public void StartDialogue(DialogueNode newDialogue)
	{
		HideChoices();
		if(newDialogue==null)
		{
			EndDialogue();
			return;
		}
        UIManager.Instance.SetActivePanel(UIManager.Instance.dialogue);
        currentDialogue=newDialogue;
		dialogueIndex=0;
		DisplayCurrentLine();
	}
	void DisplayCurrentLine()
	{
		if(currentDialogue==null)
			return;
		if(dialogueIndex<currentDialogue.dialogue.Count)
		{
			Dialogue line=currentDialogue.dialogue[dialogueIndex];
			SetValues(line.name, line.text, line.sprite);
		}
		else
			ShowChoicesOrEnd();
	}
	public void NextDialogue()
	{
		if(currentDialogue==null)
			return;
		if(AreChoicesVisible())
			return;
		dialogueIndex++;
		DisplayCurrentLine();
	}
	void ShowChoicesOrEnd()
	{
		if(currentDialogue.choices.Count>0)
			ShowChoices();
		else
			EndDialogue();
	}
	void ShowChoices()
	{
		HideChoices();
		for(int i=0; i<Mathf.Min(choices.Length, currentDialogue.choices.Count); i++)
		{
			choices[i].SetActive(true);
			TextMeshProUGUI choiceText=choices[i].GetComponent<TextMeshProUGUI>();
			if(choiceText!=null)
				choiceText.text=currentDialogue.choices[i].text;
		}
	}
	public void SelectChoice(int choiceIndex)
	{
		if(currentDialogue==null)
			return;
		if(choiceIndex<0 || choiceIndex>=currentDialogue.choices.Count)
			return;
		currentDialogue.choices[choiceIndex].onSelected.Invoke();
		DialogueNode nextNode=currentDialogue.choices[choiceIndex].nextNode;
		HideChoices();
		if(nextNode==null)
		{
			EndDialogue();
			return;
		}
		StartDialogue(nextNode);
	}
	void HideChoices()
	{
		foreach(GameObject choice in choices)
		{
			if(choice!=null)
				choice.SetActive(false);
		}
	}
	bool AreChoicesVisible()
	{
		foreach(GameObject choice in choices)
		{
			if(choice!=null && choice.activeSelf)
				return true;
		}
		return false;
	}
	void EndDialogue()
	{
        UIManager.Instance.SetActivePanel(UIManager.Instance.dialogue, false);
        currentDialogue=null;
		dialogueIndex=0;
		dialogueText.text="";
		portraitImage.sprite=null;
	}
	void SetValues(string name, string dialogue, Sprite portrait)
	{
		nameText.text=name;
		dialogueText.text=dialogue;
		portraitImage.sprite=portrait;
	}
}
