using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
	public DialogueNode currentDialogue;
	[SerializeField] float letterSpeed=.06f;
	[SerializeField] float clickCooldown=.25f;
	[Header("References")]
	[SerializeField] GameObject nameHolder;
	[SerializeField] TextMeshProUGUI nameText;
	[SerializeField] TextMeshProUGUI dialogueText;
	[SerializeField] Image portraitImage;
	[SerializeField] GameObject choiceHolder;
	[SerializeField] GameObject[] choices;
	float nextClickTime;
	int dialogueIndex;
	Coroutine typingCoroutine;
	bool isTyping;
	void Awake()
	{
		HideChoices();
	}
	public void StartDialogue(DialogueNode newDialogue)
	{
		StopTyping();
		HideChoices();
		if(newDialogue==null)
		{
			EndDialogue();
			return;
		}
		UIManager.Instance.SetActivePanel(UIManager.Instance.dialogue);
		currentDialogue=newDialogue;
		dialogueIndex=0;
		nextClickTime=Time.unscaledTime+clickCooldown;
		DisplayCurrentLine();
	}
	void DisplayCurrentLine()
	{
		if(currentDialogue==null)
			return;
		NPC npc=NPCManager.Instance.GetCurrentNPC();
		if(dialogueIndex<currentDialogue.dialogue.Count)
		{
			Dialogue line=currentDialogue.dialogue[dialogueIndex];
			SetValues(GameManager.Instance.ReplaceVariables(line.name), GameManager.Instance.ReplaceVariables(line.text), line.sprite);
			return;
		}
		ShowChoicesOrEnd();
	}
	public void NextDialogue()
	{
		if(Time.unscaledTime<nextClickTime)
			return;
		if(currentDialogue==null || AreChoicesVisible())
			return;
		nextClickTime=Time.unscaledTime+clickCooldown;
		if(isTyping)
		{
			CompleteTyping();
			return;
		}
		dialogueIndex++;
		DisplayCurrentLine();
	}
	void SetValues(string name, string dialogue, Sprite portrait)
	{
		StopTyping();
		nameHolder.SetActive(!string.IsNullOrEmpty(name));
		nameText.text=name;
		dialogueText.text=dialogue;
		portraitImage.sprite=portrait;
		typingCoroutine=StartCoroutine(TypeText());
	}
	IEnumerator TypeText()
	{
		isTyping=true;
		dialogueText.ForceMeshUpdate();
		TMP_TextInfo textInfo=dialogueText.textInfo;
		int totalCharacters=textInfo.characterCount;
		dialogueText.maxVisibleCharacters=0;
		for(int i=0; i<totalCharacters; i++)
		{
			dialogueText.maxVisibleCharacters=i+1;
			yield return new WaitForSeconds(letterSpeed);
		}
		dialogueText.maxVisibleCharacters=totalCharacters;
		isTyping=false;
		typingCoroutine=null;
	}
	void CompleteTyping()
	{
		if(typingCoroutine!=null)
		{
			StopCoroutine(typingCoroutine);
			typingCoroutine=null;
		}
		dialogueText.maxVisibleCharacters=int.MaxValue;
		isTyping=false;
	}
	void StopTyping()
	{
		if(typingCoroutine!=null)
		{
			StopCoroutine(typingCoroutine);
			typingCoroutine=null;
		}
		isTyping=false;
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
		choiceHolder.SetActive(true);
		NPC npc=NPCManager.Instance.GetCurrentNPC();
		for(int i=0; i<Mathf.Min(choices.Length, currentDialogue.choices.Count); i++)
		{
			DialogueChoice choice=currentDialogue.choices[i];
			bool available=choice.CanSelect(npc);
			if(!available && !choice.showIfUnavailable)
				continue;
			choices[i].SetActive(true);
			Button button=choices[i].GetComponent<Button>();
			if(button!=null)
				button.interactable=available;
			TextMeshProUGUI choiceText=choices[i].GetComponentInChildren<TextMeshProUGUI>();
			if(choiceText!=null)
				choiceText.text=GameManager.Instance.ReplaceVariables(available ? choice.text : choice.unavailableText);
		}
	}
	public void SelectChoice(int choiceIndex)
	{
		if(currentDialogue==null)
			return;
		if(choiceIndex<0||choiceIndex>=currentDialogue.choices.Count)
			return;
		DialogueNode originalDialogue=currentDialogue;
		DialogueChoice choice=currentDialogue.choices[choiceIndex];
		if(!choice.CanSelect(NPCManager.Instance.GetCurrentNPC()))
			return;
		DialogueNode nextNode=choice.nextNode;
		HideChoices();
		choice.onSelected.Invoke();
		if(currentDialogue!=originalDialogue)
			return;
		if(nextNode==null)
		{
			EndDialogue();
			return;
		}
		StartDialogue(nextNode);
	}
	void HideChoices()
	{
		choiceHolder.SetActive(false);
		foreach(GameObject choice in choices)
		{
			if(choice!=null)
			{
				choice.SetActive(false);
				Button button=choice.GetComponent<Button>();
				if(button!=null)
					button.interactable=true;
			}
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
		StopTyping();
		DialogueNode nextNode=currentDialogue.nextNode;
		if(nextNode==null)
		{
			UIManager.Instance.SetActivePanel(UIManager.Instance.dialogue, false);
			currentDialogue=null;
			dialogueIndex=0;
			dialogueText.text="";
			dialogueText.maxVisibleCharacters=int.MaxValue;
			portraitImage.sprite=null;
			NPCManager.Instance.ResetNPCDialogue();
			return;
		}
		StartDialogue(nextNode);
	}
}
