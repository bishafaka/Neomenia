using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject inventory;
	void Awake()
	{
		if(inventory!=null)
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
