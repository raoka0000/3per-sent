using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagUtil{
	public static readonly string[] BulletTags = { "PlayerBullet", "EnemyBullet"};
	public static int BulletTagsToInt(string a){
		if (a == "PlayerBullet") {
			return 0;
		}else if(a == "EnemyBullet"){
			return 1;
		}
		return 0;
	}

	public static bool IsOpponentTag(string a, string b){
		switch (a){
		case "Player":
			return b == "Enemy";
		case "PlayerFriend":
			return b == "Enemy";
		case "Enemy":
			return b == "Player";
		case "PlayerBullet":
			return b == "EnemyBullet";
		case "EnemyBullet":
			return b == "PlayerBullet";
		default:
			break;
		}
		return false;
	}

	public static bool IsFriendTag(string a, string b){
		switch (a){
		case "PlayerFriend":
			return b == "Player";
		case "Enemy":
			return b == "Enemy";
		case "PlayerBullet":
			return b == "Player";
		case "EnemyBullet":
			return b == "Enemy";
		default:
			break;
		}
		return false;
	}
	public static string GetFriendTag(string a){
		switch (a){
		case "Player":
			return "Player";
		case "PlayerFriend":
			return "Player";
		case "Enemy":
			return "Enemy";
		case "PlayerBullet":
			return "Player";
		case "EnemyBullet":
			return "Enemy";
		default:
			break;
		}
		return "";
	}


	public static bool IsHitBulletTag(string a, string b){
		switch (a){
		case "Player":
			return b == "EnemyBullet";
		case "PlayerFriend":
			return b == "EnemyBullet";
		case "Enemy":
			return b == "PlayerBullet";
		default:
			break;
		}
		return false;
	}

	public static bool IsHitActorTag(string a, string b){
		switch (a){
		case "Player":
			return b == "Enemy";
		case "PlayerFriend":
			return b == "Enemy";
		case "Enemy":
			return b == "Player";
		default:
			break;
		}
		return false;
	}

	public static string GetHomingActorTag(string a){
		switch (a){
		case "Player":
			return "Enemy";
		case "PlayerFriend":
			return "Enemy";
		case "Enemy":
			return "Player";
		case "PlayerBullet":
			return "Enemy";
		case "EnemyBullet":
			return "Player";
		default:
			break;
		}
		return "Player";
	}

	
}