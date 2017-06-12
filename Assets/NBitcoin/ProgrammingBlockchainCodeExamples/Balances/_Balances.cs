using NBitcoin;

namespace ProgrammingBlockchainCodeExamples
{
	public class _Balances : UnityEngine.MonoBehaviour
	{
		void Start()
		{
			BitcoinSecret bitcoinPrivateKey = new BitcoinSecret("cUwC2Dk7VvVyxF3jGyHdz5HTtxHYqHuQgWX1pnYvqckwCyUGStd3");
			QBitNinja4Unity.QBitNinjaClient.GetBalance(bitcoinPrivateKey.GetAddress(), bitcoinPrivateKey.Network, GetBalanceResponse);
		}

		void GetBalanceResponse(QBitNinja4Unity.Models.BalanceModelJson result, Network network)
		{
			UnityEngine.Debug.Log(UnityEngine.JsonUtility.ToJson(result,true));
		}
	}
}