using System;

namespace QBitNinja4Unity.Models
{
	[Serializable]
	public class BalanceModelJson
	{
		public string						continuation;
		public BalanceOperationJson[]		operations;
		public BalanceOperationJson[]		conflictedOperations;

		public BalanceModelJson Result()
		{
			return this;
		}
	}

	[Serializable]
	public class BalanceOperationJson
	{
		public int							amount;
		public int							confirmations;
		public int							height;
		public string						blockId;
		public string						transactionId;
		public CoinJson[]					receivedCoins;
		public CoinJson[]					spentCoins;
		public string						firstSeen;
	}

	[Serializable]
	public class BalanceSummaryJson
	{
		public BalanceSummaryDetailsJson	unConfirmed;
		public BalanceSummaryDetailsJson	confirmed;
		public BalanceSummaryDetailsJson	spendable;
		public BalanceSummaryDetailsJson	immature;
		public int							olderImmature;
		public string						cacheHit;

		public BalanceSummaryJson Result()
		{
			return this;
		}
	}

	[Serializable]
	public class BalanceSummaryDetailsJson
	{
		public int							transactionCount;
		public int							amount;
		public int							received;
		public AssetBalanceSummaryDetailsJson[]	assets;
	}

	[Serializable]
	public class AssetBalanceSummaryDetailsJson
	{
		public string						asset;
		public long							quantity;
		public long							received;
	}
}
