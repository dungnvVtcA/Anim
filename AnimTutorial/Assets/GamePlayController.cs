using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
	public static GamePlayController instance;

	public GameObject player1;

	public GameObject Player2;

    void Start()
    {
        
    }

    public void CheckCollider(TypePlayer type , int dmg)
	{
		
		var x1 = player1.transform.localPosition.x;
		var x2 = Player2.transform.localPosition.x;
		if(x1 > x2)
		{
			if (Mathf.Abs(x1 - x2) <= 0.35f)
			{
				SetupdataHp(type , dmg);
			}
		}
		else
		{
			if (Mathf.Abs(x2 - x1) <= 0.35f)
			{
				SetupdataHp(type,dmg);
			}
		}
		
	}
	public void SetupdataHp(TypePlayer type , float dmg)
	{
		Debug.Log("va cham");
		switch (type)
		{
			case TypePlayer.Blue:
				player1.transform.GetComponent<HeavyBandit>().hp.UpdateHp(dmg);
				break;
			case TypePlayer.Red:
				Player2.transform.GetComponent<HeavyBandit>().hp.UpdateHp(dmg);
				break;
		}
	}
}
