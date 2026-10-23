using UnityEngine;

public class Pickup : MonoBehaviour
{
	public int amount=1;
	[SerializeField] Item item;
	[SerializeField] SpriteRenderer spriteRenderer;
	public void InitItem(Item newItem, int quantity)
	{
		item=newItem;
		amount=quantity;
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
		amount=UIManager.Instance.inventoryManager.AddItem(item, amount);
		if(amount<=0)
			Destroy(gameObject);
	}
}
