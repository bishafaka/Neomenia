using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public GameObject inventoryItemPrefab;
	public InventorySlot[] InventorySlots;
	public bool AddItem(Item item, int quantity=1)
	{
		foreach (InventorySlot slot in InventorySlots)
		{
			InventoryItem itemInSlot=slot.GetComponentInChildren<InventoryItem>();
			if(itemInSlot!=null && itemInSlot.item==item && itemInSlot.amount+quantity<=item.maxStack)
			{
				itemInSlot.UpdateAmount(quantity);
				return true;
			}
		}
		foreach (InventorySlot slot in InventorySlots)
		{
			InventoryItem itemInSlot=slot.GetComponentInChildren<InventoryItem>();
			if(itemInSlot==null)
			{
				SpawnItem(item, slot, quantity);
				return true;
			}
		}
		return false;
	}
	void SpawnItem(Item item, InventorySlot slot, int quantity)
	{
		GameObject newItem=Instantiate(inventoryItemPrefab, slot.transform);
		InventoryItem inventoryItem=newItem.GetComponent<InventoryItem>();
		inventoryItem.InitItem(item, quantity);
	}
}
