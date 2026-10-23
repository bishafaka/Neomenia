using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public GameObject inventoryItemPrefab;
	public InventorySlot[] InventorySlots;
	public bool AddItem(Item item)
	{
		for(int i=0; i<InventorySlots.Length; i++)
		{
			InventorySlot slot=InventorySlots[i];
			InventoryItem itemInSlot=slot.GetComponentInChildren<InventoryItem>();
			if(itemInSlot==null)
			{
				SpawnItem(item, slot);
				return true;
			}
		}
		return false;
	}
	void SpawnItem(Item item, InventorySlot slot)
	{
		GameObject newItem=Instantiate(inventoryItemPrefab, slot.transform);
		InventoryItem inventoryItem=newItem.GetComponent<InventoryItem>();
		inventoryItem.InitItem(item);
	}
}
