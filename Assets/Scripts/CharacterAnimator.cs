using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAnimator : MonoBehaviour
{
	private const float locomotionAnimationSmoothTime = 0.1f;

	private NavMeshAgent agent;
	private Animator animator;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		var speedPercent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
	}
}
