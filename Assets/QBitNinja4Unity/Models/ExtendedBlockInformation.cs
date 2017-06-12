using System;

namespace QBitNinja4Unity.Models
{
	[Serializable]
	public class ExtendedBlockInformation
	{
		public int		size;
		public int		strippedSize;
		public int		transactionCount;
		public int		blockSubsidy;
		public int		blockReward;

		static public QBitNinja.Models.ExtendedBlockInformation Create(ExtendedBlockInformation block)
		{
			var extendedBlockInformation = new QBitNinja.Models.ExtendedBlockInformation();

			extendedBlockInformation.Size				= block.size;
			extendedBlockInformation.StrippedSize		= block.strippedSize;
			extendedBlockInformation.TransactionCount	= block.transactionCount;
			extendedBlockInformation.BlockSubsidy		= new NBitcoin.Money(block.blockSubsidy);
			extendedBlockInformation.BlockReward		= new NBitcoin.Money(block.blockReward);

			return extendedBlockInformation;
		}
	}
}