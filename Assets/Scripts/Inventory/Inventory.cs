using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton
	public static Inventory instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Mare than one instance of Inventory found!");
			return;
		}

		instance = this;
	}
	#endregion

	public int space = 20;

	public List<Item> items = new List<Item>();

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public bool Add(Item item)
	{
		if (!item.isDefaultItem)
		{
			if (items.Count >= space)
			{
				Debug.Log("Not enough room.");
				return false;
			}

			items.Add(item);

			onItemChangedCallback?.Invoke();
		}

		return true;
	}

	public void Remove(Item item)
	{
		items.Remove(item);

		onItemChangedCallback?.Invoke();
	}
}
