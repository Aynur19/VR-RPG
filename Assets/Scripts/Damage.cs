using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;


public class Damage : MonoBehaviour
{
	//public Collider target;
	public EnemyController enemyController;


	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name == "Enemy")
		{
			enemyController.animator.SetBool("isDied", true);

			StartCoroutine(Restart(3));
		}
	}

	private IEnumerator Restart(int delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
