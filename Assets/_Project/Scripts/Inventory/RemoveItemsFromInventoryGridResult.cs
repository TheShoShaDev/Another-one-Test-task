namespace Inventory
{
	public readonly struct RemoveItemsFromInventoryGridResult
	{
		public readonly int ItemsToRemoveAmount;
		public readonly bool Success;

		public RemoveItemsFromInventoryGridResult(int itemsToRemoveAmount, bool success)
		{
			ItemsToRemoveAmount = itemsToRemoveAmount;
			Success = success;
		}
	}
}
