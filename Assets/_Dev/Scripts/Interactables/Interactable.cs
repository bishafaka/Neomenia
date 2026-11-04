using UnityEngine;

public class Interactable : MonoBehaviour
{
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
