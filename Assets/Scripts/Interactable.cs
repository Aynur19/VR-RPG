using UnityEngine;

public class Interactable : MonoBehaviour
{
	public Transform interactionTransform;
	public float radius = 3f;

	private bool isFocus = false;
	private bool hasInteracted = false;
	private Transform player;

	private void Update()
	{
		if (isFocus && !hasInteracted)
		{
			var distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public virtual void Interact()
	{
		Debug.Log($"Interacting with {transform.name}");
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	private void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
		{
			interactionTransform = transform;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}
