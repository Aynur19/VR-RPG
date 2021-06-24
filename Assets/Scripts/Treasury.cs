using UnityEngine;

public class Treasury : MonoBehaviour
{
	private Transform transform;
	public float radius = 4f;

	private void OnDrawGizmosSelected()
	{

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
