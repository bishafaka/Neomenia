using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] InventorySlot[] inventorySlots;
    public int AddItem(Item item, int quantity, bool requireFullAmount=false)
    {
        if(item==null || quantity<=0)
            return quantity;
        int availableSpace=0;
        if(requireFullAmount)
        {
            foreach(InventorySlot slot in inventorySlots)
            {
                InventoryItem current=slot.GetComponentInChildren<InventoryItem>();
                if(current==null)
                    availableSpace+=item.maxStack;
                else if(current.item==item)
                    availableSpace+=Mathf.Max(0, item.maxStack-current.amount);
            }
            if(availableSpace<quantity)
                return quantity;
        }
        foreach(InventorySlot slot in inventorySlots)
        {
            InventoryItem current=slot.GetComponentInChildren<InventoryItem>();
            if(current==null || current.item!=item)
                continue;
            int added=Mathf.Min(item.maxStack-current.amount, quantity);
            if(added<=0)
                continue;
            current.UpdateAmount(added);
            quantity-=added;
            if(quantity==0)
                return 0;
        }
        foreach(InventorySlot slot in inventorySlots)
        {
            if(slot.GetComponentInChildren<InventoryItem>()!=null)
                continue;
            int added=Mathf.Min(item.maxStack, quantity);
            SpawnItem(item, slot, added);
            quantity-=added;
            if(quantity==0)
                return 0;
        }
        return quantity;
    }
    void SpawnItem(Item item, InventorySlot slot, int quantity)
    {
        InventoryItem newItem=Instantiate(inventoryItemPrefab, slot.transform).GetComponent<InventoryItem>();
        newItem.InitItem(item, quantity);
    }
}
