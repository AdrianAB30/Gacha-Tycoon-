using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int month;
    public int moneyAdminister;
    public float userSatisfaction;

    public TMP_Text reportPanelText;
    public TMP_Text reportSatisfactionFinalText;

    private void Start()
    {
        month = 1;
        moneyAdminister = 50;
        userSatisfaction = 100f;
        UpdateReportPanel(); 
    }

    public void TakeDecision(Decision decision)
    {
        moneyAdminister -= decision.cost;
        userSatisfaction += decision.satisfactionImpact;

        UpdateReportPanel();
    }

    private void UpdateReportPanel()
    {
        reportPanelText.text = "Mes: " + month + "\n\n\n\nDinero: " + moneyAdminister + "\n\n\n\nSatisfacción: " + userSatisfaction + "%";
    }

    private void EndHalfYearReport()
    {
        if (userSatisfaction < 50)
        {
            reportSatisfactionFinalText.text = "Te has ido a la bancarrota. ¡Juego Terminado!";
        }
        else if (userSatisfaction < 75)
        {
            reportSatisfactionFinalText.text = "Lograste mantenerte a flote, pero la satisfacción del usuario es baja.";
        }
        else
        {
            reportSatisfactionFinalText.text = "¡Ganaste! Mantuviste a los usuarios satisfechos.";
        }
    }
}
