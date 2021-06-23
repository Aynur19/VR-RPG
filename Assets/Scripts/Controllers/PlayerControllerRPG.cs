
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerControllerRPG : MonoBehaviour
	{
		public Interactable focus;
		public LayerMask movementMask;

		private Camera mainCamera;
		private PlayerMotor motor;

		private void Start()
		{
			mainCamera = Camera.main;
			motor = GetComponent<PlayerMotor>();
		}

		private void Update()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100, movementMask))
				{
					motor.MoveToPoint(hit.point);

					RemoveFocus();
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100))
				{
					var interactable = hit.collider.GetComponent<Interactable>();

					if (interactable != null)
					{
						SetFocus(interactable);
					}
				}
			}
		}

		private void SetFocus(Interactable newFocus)
		{
			if (newFocus != focus)
			{
				if (focus != null)
				{
					focus.OnDefocused();
				}

				focus = newFocus;
				motor.FollowTarget(focus);
			}

			focus.OnFocused(transform);
		}

		private void RemoveFocus()
		{
			if (focus != null)
			{
				focus.OnDefocused();
			}

			focus = null;
			motor.StopFollowingTarget();
		}
	}
}