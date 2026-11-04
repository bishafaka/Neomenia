using UnityEngine;

public class Building : MonoBehaviour
{
	public Transform entranceTransform;
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(gameObject);
	}
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
			other.GetComponent<PlayerInteract>().SetInteractable(null);
	}
}
