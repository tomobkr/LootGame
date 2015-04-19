using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIUpdate : MonoBehaviour {

    string GoldScorePrefix = "Score : ";
	
    public void GoldScoreUpdate()
    {
        var GoldScoreUI = GameObject.FindGameObjectWithTag("GoldScore").GetComponent<Text>();

        var PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        GoldScoreUI.text = GoldScorePrefix + PlayerStats.GoldCount.ToString();
    }
}
