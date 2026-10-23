using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName="Inventory/Item")]
public class Item : ScriptableObject
{
	public string itemID;
	public string itemName;
	public Sprite icon;
	public float price;
	public int maxStack=99;
}
