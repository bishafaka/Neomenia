using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public GameObject inventoryItemPrefab;
	public InventorySlot[] InventorySlots;
	public bool AddItem(Item item, int quantity=1)
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
			if(itemInSlot.item==item && itemInSlot.amount+quantity<=item.maxStack)
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
