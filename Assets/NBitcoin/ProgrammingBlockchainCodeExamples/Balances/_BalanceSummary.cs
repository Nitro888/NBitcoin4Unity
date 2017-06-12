using NBitcoin;

namespace ProgrammingBlockchainCodeExamples
{
public class _BalanceSummary : UnityEngine.MonoBehaviour
	{
		void Start()
		{
			BitcoinSecret bitcoinPrivateKey = new BitcoinSecret("cUwC2Dk7VvVyxF3jGyHdz5HTtxHYqHuQgWX1pnYvqckwCyUGStd3");
			QBitNinja4Unity.QBitNinjaClient.GetBalanceSummary(bitcoinPrivateKey.GetAddress(), bitcoinPrivateKey.Network, GetBalanceSummaryResponse);
		}

		void GetBalanceSummaryResponse(QBitNinja4Unity.Models.BalanceSummaryJson result, NBitcoin.Network network)
		{
			UnityEngine.Debug.Log(UnityEngine.JsonUtility.ToJson(result,true));
		}
	}
}