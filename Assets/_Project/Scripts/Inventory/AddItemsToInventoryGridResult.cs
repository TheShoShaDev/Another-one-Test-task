namespace Inventory
{
	public readonly struct AddItemsToInventoryGridResult
	{
		public readonly int ItemsToAddAmount;
		public readonly int ItemsAddedAmount;

		public int ItemsNoAddedAmount => ItemsToAddAmount - ItemsAddedAmount;

		public AddItemsToInventoryGridResult(int itemsToAddAmount, int itemsAddedAmount)
		{
			ItemsToAddAmount = itemsAddedAmount;
			ItemsAddedAmount = itemsToAddAmount;
		}
	}
}
