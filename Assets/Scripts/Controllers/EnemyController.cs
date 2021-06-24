using System.Collections;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
	public float lookRadius = 10f;
	public Treasury treasury;

	public Animator animator;

	private Transform target;
	private NavMeshAgent agent;
	private bool isAttack = false;

	private void Start()
	{
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		//Debug.Log($"{agent.name}");
	}

	private void Update()
	{
		var distance = Vector3.Distance(target.position, transform.position);
		if (distance <= lookRadius)
		{
			//Debug.Log($"Agent name: {agent.name}");
			//Debug.Log($"Target name: {target.name}");
			agent.SetDestination(target.position);
			Debug.Log($"{target.position}");
			FaceTarget();

			if (distance <= agent.stoppingDistance)
			{
				Debug.Log($"isAttack: {isAttack}");
				if (!isAttack)
				{
					isAttack = true;
					animator.SetBool("isAttack", isAttack);
					
					StartCoroutine(Cooldown(2));
				}
				//Attack the target
				//Face the target
			}
		}
	}

	private IEnumerator Cooldown(int delay)
	{
		yield return new WaitForSeconds(delay);
		isAttack = false;

	}

	private void FaceTarget()
	{
		var direction = (target.position - transform.position).normalized;
		var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
