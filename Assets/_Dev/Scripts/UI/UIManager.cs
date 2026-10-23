using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public InventoryManager inventoryManager;
    [Header("Panels")]
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

        if (inventory!=null)
			inventory.SetActive(false);
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
    void ToggleInventory() => inventory.SetActive(!inventory.activeSelf);
}
