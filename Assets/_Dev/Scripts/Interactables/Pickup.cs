using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField] Item item;
	[SerializeField] SpriteRenderer spriteRenderer;
    public void InitItem(Item newItem)
    {
        item=newItem;
        spriteRenderer.sprite=newItem.icon;
    }
    void Awake()
    {
        if(spriteRenderer==null)
            spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }
    void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			PickupItem();
	}
	void PickupItem()
	{
		if(UIManager.Instance.inventoryManager.AddItem(item))
			Destroy(gameObject);
	}
}
