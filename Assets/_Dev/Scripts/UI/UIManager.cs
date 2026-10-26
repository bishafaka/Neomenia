using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }
	public InventoryManager inventoryManager;
	public DialogueManager dialogueManager;
	[Header("References")]
    public GameObject dialogue;
    public GameObject inventory;
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
	public void SetActivePanel(GameObject panel, bool wantsToBeActive=true)
	{
        panel.SetActive(wantsToBeActive);
        if(panel.activeSelf)
            InputManager.Instance.SetPlayerStateUI();
        else
            InputManager.Instance.SetPlayerStatePlayer();
    }
}
