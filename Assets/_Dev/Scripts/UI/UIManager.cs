using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }
	public InventoryManager inventoryManager;
	public DialogueManager dialogueManager;
	[Header("References")]
	[SerializeField] GameObject dialogue;
	[SerializeField] GameObject inventory;
	void Awake()
	{
		if(Instance==null)
		{
			Instance=this;
			DontDestroyOnLoad(this);
		}
		else
			Destroy(gameObject);
		if(inventory!=null)
			inventory.SetActive(false);
		if(dialogue != null)
			dialogue.SetActive(false);
	}
	void OnEnable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.InventoryPressed+=ToggleInventory;
	}
	void OnDisable()
	{
		if(InputManager.Instance!=null)
			InputManager.Instance.InventoryPressed-=ToggleInventory;
	}
	void ToggleInventory()
	{
		inventory.SetActive(!inventory.activeSelf);
        if(inventory.activeSelf)
            InputManager.Instance.SetPlayerStateUI();
		else
			InputManager.Instance.SetPlayerStatePlayer();
    }
	public void ToggleDialogue()
	{
		dialogue.SetActive(!dialogue.activeSelf);
        if(dialogue.activeSelf)
            InputManager.Instance.SetPlayerStateUI();
        else
            InputManager.Instance.SetPlayerStatePlayer();
    }
}
