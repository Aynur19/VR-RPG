using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
	public static EquipmentManager instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChangedCallback;
	public SkinnedMeshRenderer targetSkinnedMesh;
	public Equipment[] defaultItems;

	private Equipment[] currentEquipment;
	private SkinnedMeshRenderer[] currentSkinnedMeshes;
	private Inventory inventory;

	private void Start()
	{
		inventory = Inventory.instance;

		var numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
		currentSkinnedMeshes = new SkinnedMeshRenderer[numSlots];

		EquipDefaultItems();
	}

	public void Equip(Equipment newItem)
	{
		var slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = Unequip(slotIndex);

		onEquipmentChangedCallback?.Invoke(newItem, oldItem);
		SetEquipmentBlendShapes(newItem, 100);

		currentEquipment[slotIndex] = newItem;
		var newSkinnedMesh = Instantiate<SkinnedMeshRenderer>(newItem.skinnedMesh);
		newSkinnedMesh.transform.parent = targetSkinnedMesh.transform;

		newSkinnedMesh.bones = targetSkinnedMesh.bones;
		newSkinnedMesh.rootBone = targetSkinnedMesh.rootBone;

		currentSkinnedMeshes[slotIndex] = newSkinnedMesh;
	}

	public Equipment Unequip(int slotIndex)
	{
		if (currentEquipment[slotIndex] != null)
		{
			if (currentSkinnedMeshes[slotIndex] != null)
			{
				Destroy(currentSkinnedMeshes[slotIndex].gameObject);
			}

			Equipment oldItem = currentEquipment[slotIndex];
			SetEquipmentBlendShapes(oldItem, 0);
			inventory.Add(oldItem);

			currentEquipment[slotIndex] = null;
	
			onEquipmentChangedCallback?.Invoke(null, oldItem);

			return oldItem;
		}

		return null;
	}

	public void UnequipAll()
	{
		for (int i = 0; i < currentEquipment.Length; i++)
		{
			Unequip(i);
		}

		EquipDefaultItems();
	}

	private void EquipDefaultItems()
	{
		foreach (var item in defaultItems)
		{
			Equip(item);
		}
	}

	private void SetEquipmentBlendShapes(Equipment item, int weight)
	{
		foreach (var blendShape in item.coveredMeshRegions)
		{
			targetSkinnedMesh.SetBlendShapeWeight((int)blendShape, weight);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			UnequipAll(); 
		}	
	}
}
