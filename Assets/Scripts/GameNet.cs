using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNet
{
	public void InvokeRequestInfo(Action onFinished)
	{
		Action<RequestInfoResponse> callback = (res) => 
		{
			{
				GamePlayerInfo info = new GamePlayerInfo();
				info.Id    = res.PlayerInfo.Id;
				info.Level = res.PlayerInfo.Level;
				info.Exp   = res.PlayerInfo.Exp;
				info.Gold  = res.PlayerInfo.Gold;
				Game.Info.Player = info;
			}

			foreach (var info in res.Skills)
			{
				GameSkillInfo skillInfo = new GameSkillInfo();
				skillInfo.SkillId = info.SkillId;
				skillInfo.Amount  = info.Amount;
				skillInfo.Level   = info.Level;
				Game.Info.Skills.Add(skillInfo.SkillId, skillInfo);
			}

			if (onFinished != null)
				onFinished();
		};

		GameAWS.InvokeLambdaAsync<RequestInfoResponse> ("requestInfo", IdentityIdPayload.New(), callback);
	}

	public void InvokeBattleReward(Action onFinished)
	{
		Action<BattleRewardResponse> callback = (res) =>
		{
			Game.Info.Player.Gold = res.Gold;
			Game.Info.Player.Exp  = res.Exp;

			if (onFinished != null)
				onFinished();
		};
		GameAWS.InvokeLambdaAsync<BattleRewardResponse> ("ewBattleReward", IdentityIdPayload.New(), callback);
	}
}

[Serializable]
public struct IdentityIdPayload
{
	public string Id;
	public static IdentityIdPayload New()
	{
		var payLoad = new IdentityIdPayload ();
		payLoad.Id = GameAWS.Credentials.GetIdentityId ();
		return payLoad;
	}

};

[Serializable]
public struct RequestInfoResponse
{
	public ResPlayerInfo PlayerInfo;
	public List<ResSkill> Skills;

	[Serializable]
	public struct ResPlayerInfo
	{
		public string Id;
		public int Level;
		public int Exp;
		public int Gold;
	}

	[Serializable]
	public struct ResSkill
	{
		public int SkillId;
		public int Level;
		public int Amount;
	}
}

[Serializable]
public struct BattleRewardResponse
{
	public int Gold;
	public int Exp;
}