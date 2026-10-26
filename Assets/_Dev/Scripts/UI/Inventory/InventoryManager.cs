using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] GameObject inventoryItemPrefab;
	[SerializeField] InventorySlot[] inventorySlots;
	Dictionary<Item, int> itemCounts=new();
	public int AddItem(Item item, int quantity, bool requireFullAmount=false)
	{
		if(item==null || quantity<=0)
			return quantity;
		if(requireFullAmount && GetAvailableSpace(item)<quantity)
			return quantity;
		int remaining=quantity;
		foreach(InventorySlot slot in inventorySlots)
		{
			InventoryItem current=slot.GetComponentInChildren<InventoryItem>();
			if(current==null || current.item!=item)
				continue;
			int added=Mathf.Min(item.maxStack-current.amount, remaining);
			if(added<=0)
				continue;
			current.UpdateAmount(added);
			remaining-=added;
			if(remaining==0)
				break;
		}
		foreach(InventorySlot slot in inventorySlots)
		{
			if(remaining==0)
				break;
			if(slot.GetComponentInChildren<InventoryItem>()!=null)
				continue;
			int added=Mathf.Min(item.maxStack, remaining);
			SpawnItem(item, slot, added);
			remaining-=added;
		}
		int addedTotal=quantity-remaining;
		if(addedTotal>0)
			ChangeItemCount(item, addedTotal);
		return remaining;
	}
	public int RemoveItem(Item item, int quantity)
	{
		if(item==null || quantity<=0)
			return quantity;
		int remaining=quantity;
		foreach (InventorySlot slot in inventorySlots)
		{
			if(remaining==0)
				break;
			InventoryItem current=slot.GetComponentInChildren<InventoryItem>();
			if(current==null || current.item!=item)
				continue;
			int removed=Mathf.Min(current.amount, remaining);
			current.UpdateAmount(-removed);
			remaining-=removed;
			if(current.amount<=0)
				Destroy(current.gameObject);
		}
		int removedTotal=quantity-remaining;
		if(removedTotal>0)
			ChangeItemCount(item, -removedTotal);
		return remaining;
	}
	public int GetItemCount(Item item)
	{
		if(item==null)
			return 0;
		return itemCounts.TryGetValue(item, out int count) ? count : 0;
	}
	void ChangeItemCount(Item item, int change)
	{
		int newCount=GetItemCount(item)+change;
		if(newCount<=0)
			itemCounts.Remove(item);
		else
			itemCounts[item]=newCount;
	}
	int GetAvailableSpace(Item item)
	{
		int space=0;
		foreach(InventorySlot slot in inventorySlots)
		{
			InventoryItem current=slot.GetComponentInChildren<InventoryItem>();
			if(current==null)
				space+=item.maxStack;
			else if(current.item==item)
				space+=Mathf.Max(0, item.maxStack-current.amount);
		}
		return space;
	}
	void SpawnItem(Item item, InventorySlot slot, int quantity)
	{
		InventoryItem newItem=Instantiate(inventoryItemPrefab, slot.transform).GetComponent<InventoryItem>();
		newItem.InitItem(item, quantity);
	}
}
