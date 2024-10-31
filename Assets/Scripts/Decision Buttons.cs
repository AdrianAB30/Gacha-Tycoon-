using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionButtons : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Decision addGachasDecision;
    [SerializeField] private Decision addAdsDecision;
    [SerializeField] private Decision raisePriceDecision;

    public void OnAddGachasClicked()
    {
        gameManager.TakeDecision(addGachasDecision);
    }

    public void OnAddAdsClicked()
    {
        gameManager.TakeDecision(addAdsDecision);
    }

    public void OnRaisePriceClicked()
    {
        gameManager.TakeDecision(raisePriceDecision);
    }
}
