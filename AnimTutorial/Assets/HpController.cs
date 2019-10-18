using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpController : MonoBehaviour
{
	[SerializeField]
	private Image Hp;

	[SerializeField]
	private Text textHp;

	public HPType type;

	public float maxHp;

	public float hpcurrent;
	private void Awake()
	{
		SetupData();

	}
	
	private void SetupData()
	{
		textHp.text = "100%";
		Hp.fillAmount = 1;
		hpcurrent = maxHp;
	}
	public void UpdateHp(float dmg = 100f)
	{
		hpcurrent -= dmg;
		Hp.fillAmount = hpcurrent / maxHp;
		if (hpcurrent < 0) hpcurrent = 0;
		textHp.text = (hpcurrent / maxHp * 100).ToString()+"%"; 
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum HPType { red,blue}