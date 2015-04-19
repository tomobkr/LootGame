using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider))]
public class ChestUse : MonoBehaviour, IInteractable {

    public int GoldCount;

    public void Interact(GameObject player)
    {
         PlayerStats Stats = GameObject.Find("Player").GetComponent<PlayerStats>();

        Stats.GoldCount += GoldCount;
        GoldCount = 0;
        UIUpdate();
    }

    void UIUpdate()
    {
        var UIUpdate = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIUpdate>();
        UIUpdate.GoldScoreUpdate();
    }
}