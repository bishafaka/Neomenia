using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIntroController : MonoBehaviour
{
	[SerializeField] TMP_InputField nameInputField;
	[SerializeField] int minWords=3;
	[SerializeField] Button theyButton;
	[SerializeField] Button heButton;
	[SerializeField] Button sheButton;
	[SerializeField] Button doneButton;
	[SerializeField] Color normalColor=Color.white;
	[SerializeField] Color clickedColor=Color.gray;
	bool hasSelectedButton=false;
	int buttonSelected=0;
	void OnEnable()
	{
		doneButton.interactable=false;
	}
	public void CheckPlayerName(string value)
	{
		string newValue=System.Text.RegularExpressions.Regex.Replace(value, @"\s+", "");
		if(value!=newValue)
			nameInputField.SetTextWithoutNotify(newValue);
		CheckConditions();
	}
	public void CheckPlayerPronouns(Button clickedButton)
	{
		hasSelectedButton=true;
		theyButton.image.color=normalColor;
		heButton.image.color=normalColor;
		sheButton.image.color=normalColor;
		clickedButton.image.color=clickedColor;
		if(clickedButton==theyButton)
			buttonSelected=0;
		if(clickedButton==heButton)
			buttonSelected=1;
		if(clickedButton==sheButton)
			buttonSelected=2;
		CheckConditions();
	}
	void CheckConditions()
	{
		bool hasEnoughWords=nameInputField.text.Length>=minWords;
		doneButton.interactable=hasEnoughWords && hasSelectedButton;
	}
	public void SaveChoices()
	{
		GameManager.Instance.SetPlayerInfo(nameInputField.text, buttonSelected);
		UIManager.Instance.SetActivePanel(gameObject, false);
	}
}
