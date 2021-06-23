using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	public EquipmentSlot equipSlot;
	public SkinnedMeshRenderer skinnedMesh;
	public EquipmentMeshRegion[] coveredMeshRegions;

	public int armorModifier;
	public int damageModifier;

	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);
		RemoveFromInventory();
	}
}

public enum EquipmentSlot
{
	Head,
	Chest,
	Legs,
	Weapon,
	Shield,
	Feet,
}

// Corresponds to body blendshapes
public enum EquipmentMeshRegion
{
	Legs,
	Arms,
	Torso,
}