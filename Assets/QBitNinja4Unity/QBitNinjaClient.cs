using NBitcoin;
using UniRx;
using System.Text;
using UnityEngine;

namespace QBitNinja4Unity
{
	public static class QBitNinjaClient
	{
		const string	MAIN	= "http://api.qbit.ninja/";
		const string	TEST	= "http://tapi.qbit.ninja/";

		static System.Collections.Generic.Dictionary<string, string> HEADER()
		{
			var header = new System.Collections.Generic.Dictionary<string, string>();
			header["Content-Type"] = "application/octet-stream";
			return header;
		}

		static string URL(NBitcoin.Network network, string option)
		{
			return ((network == NBitcoin.Network.Main) ? MAIN : TEST) + option;
		}

		static string EscapeUrlPart(string str)
		{
			return System.Web.NBitcoin.HttpUtility.UrlEncode(str);
		}

		static string CreateParameters(params object[] parameters)
		{
			var builder = new StringBuilder();
			for (int i = 0; i<parameters.Length - 1; i += 2)
			{
				if (parameters[i + 1] == null)
					continue;
				builder.Append(parameters[i].ToString() + "=" + parameters[i + 1].ToString() + "&");
			}
			if (builder.Length == 0)
				return "";
			var result = builder.ToString();
			return "?" + result.Substring(0, result.Length - 1);
		}

		public delegate void GetTransactionResponse(QBitNinja.Client.Models.GetTransactionResponse result, NBitcoin.Network network);
		static public void GetTransaction(uint256 transactionId, NBitcoin.Network network, GetTransactionResponse response)
		{
			ObservableWWW.Get(URL(network, "transactions/" + EscapeUrlPart(transactionId.ToString()))).
							Subscribe(	x 	=>	response(JsonUtility.FromJson<Models.GetTransactionResponse>(x).Result(),network),
										ex	=>	Debug.Log("error : " + ex.Message));
		}

		public delegate void BroadcastResponse(QBitNinja.Client.Models.BroadcastResponse result, NBitcoin.Network network);
		static public void Broadcast(Transaction transaction, NBitcoin.Network network, BroadcastResponse response)
		{
			ObservableWWW.Post(URL(network, "transactions"), transaction.ToBytes(), HEADER()).
							Subscribe(	x   =>	response(JsonUtility.FromJson<Models.BroadcastResponse>(x).Result(),network),
										ex	=>	Debug.Log("error : " + ex.Message));
		}

		public delegate void GetBlockResponse(QBitNinja.Client.Models.GetBlockResponse result, NBitcoin.Network network);
		static public void GetBlock(QBitNinja.Client.Models.BlockFeature blockFeature, NBitcoin.Network network, GetBlockResponse response, bool headerOnly = false, bool extended = false)
		{
			ObservableWWW.Get(URL(network, "blocks/" + EscapeUrlPart(blockFeature.ToString()) + CreateParameters("headerOnly",headerOnly,"extended",extended))).
			             	Subscribe(	x   =>	response(JsonUtility.FromJson<Models.GetBlockResponse>(x).Result(),network),
										ex	=>	Debug.Log("error : " + ex.Message));
		}

		public delegate void BlockHeaderResponse(QBitNinja.Client.Models.WhatIsBlockHeader result, NBitcoin.Network network);
		static public void BlockHeader(QBitNinja.Client.Models.BlockFeature blockFeature, NBitcoin.Network network, BlockHeaderResponse response)
		{
			ObservableWWW.Get(URL(network, "blocks/" + EscapeUrlPart(blockFeature.ToString()) + "/header")).
                            Subscribe(	x   =>	response(JsonUtility.FromJson<Models.WhatIsBlockHeader>(x).Result(),network),
										ex	=>	Debug.Log("error : " + ex.Message));
		}

		public delegate void GetBalanceResponse(QBitNinja.Client.Models.BalanceModel result, NBitcoin.Network network);
		static public void GetBalance(BitcoinAddress address, NBitcoin.Network network, GetBalanceResponse response, bool includeImmature = false, bool unspentOnly = false, bool colored = true)
		{
			ObservableWWW.Get(URL(network, "balances/" + EscapeUrlPart(address.ToString()) + CreateParameters("includeImmature", includeImmature, "unspentOnly", unspentOnly, "colored", colored))).
			             Subscribe(	x   =>	response(JsonUtility.FromJson<Models.BalanceModel>(x).Result(),network),
			                	      	ex	=>	Debug.Log("error : " + ex.Message));
		}

		public delegate void GetBalanceSummaryResponse(QBitNinja.Client.Models.BalanceSummary result, NBitcoin.Network network);
		static public void GetBalanceSummary(BitcoinAddress address, NBitcoin.Network network, GetBalanceSummaryResponse response, bool colored = true)
		{
			ObservableWWW.Get(URL(network, "balances/" + EscapeUrlPart(address.ToString()) + "/summary" + CreateParameters("colored", colored))).
			             	Subscribe(	x	=> response(JsonUtility.FromJson<Models.BalanceSummary>(x).Result(),network),
										ex	=>	Debug.Log("error : " + ex.Message));
		}
		static public void GetBalanceSummary(string address, NBitcoin.Network network, GetBalanceSummaryResponse response, bool colored = true)
		{
			ObservableWWW.Get(URL(network, "balances/" + EscapeUrlPart(address) + "/summary" + CreateParameters("colored", colored))).
							 Subscribe(x => response(JsonUtility.FromJson<Models.BalanceSummary>(x).Result(), network),
										ex => Debug.Log("error : " + ex.Message));
		}
	}
}