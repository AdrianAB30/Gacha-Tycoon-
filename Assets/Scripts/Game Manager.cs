using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int month;
    public int moneyAdminister;
    public float userSatisfaction;
    public TMP_Text reportPanelText;
    public TMP_Text reportSatisfactionFinalText;
    public TMP_Text moneyText;
    public TMP_Text monthText; 
    public Image finalImageMonth; 
    private int decisionCount = 0;

    private void Start()
    {
        ResetGame();
        ShowInitialMonthText(); 
        Debug.Log("Dinero inicial: " + moneyAdminister);
    }

    private void ShowInitialMonthText()
    {
        monthText.text = "Mes " + month;
        monthText.gameObject.SetActive(true);
        StartCoroutine(FadeOutInitialMonthText());
    }

    private IEnumerator FadeOutInitialMonthText()
    {
        Color textColor = monthText.color;
        textColor.a = 1; 
        monthText.color = textColor;

        for (float t = 1; t >= 0; t -= Time.deltaTime)
        {
            textColor.a = t;
            monthText.color = textColor;
            yield return null;
        }
        monthText.gameObject.SetActive(false);
    }
    public void TakeDecision(Decision decision)
    {
        moneyAdminister -= decision.cost;

        if (moneyAdminister < 0)
        {
            moneyAdminister = 0;
        }

        userSatisfaction += decision.satisfactionImpact;

        UpdateMoneyText();
        UpdateReportPanel();

        decisionCount++;

        if (decisionCount >= 3)
        {
            ShowEndReport();
            decisionCount = 0;
        }
    }

    private void ShowEndReport()
    {
        EndHalfYearReport();
        Invoke("ShowMonthText", 2f);
        Invoke("AdvanceMonth", 2f);
    }

    public void AdvanceMonth()
    {
        month++;
        Debug.Log("Mes: " + month);

        ResetMonthValues();

        ShowMonthText();
    }

    private void EndMonth()
    {
        reportPanelText.text = "";
        reportSatisfactionFinalText.text = "";
        UpdateReportPanel();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "DINERO: $" + moneyAdminister;
    }

    private void UpdateReportPanel()
    {
        reportPanelText.text = "Mes: " + month + "\n\n\nDinero: " + moneyAdminister + "\n\n\nSatisfacción: " + userSatisfaction + "%";
    }

    private void UpdateMonthText()
    {
        monthText.text = "Mes " + month;
        monthText.gameObject.SetActive(true);
    }

    private void EndHalfYearReport()
    {
        if (userSatisfaction < 40)
        {
            reportSatisfactionFinalText.text = "Te has ido a la quiebra";
        }
        else if (userSatisfaction < 60)
        {
            reportSatisfactionFinalText.text = "Lograste mantenerte, pero la satisfacción del usuario es baja.";
        }
        else
        {
            reportSatisfactionFinalText.text = "Mantuviste a los usuarios satisfechos.";
        }
    }

    public void ResetGame()
    {
        month = 1;
        moneyAdminister = 50;
        userSatisfaction = 100f;

        UpdateMoneyText();
        UpdateReportPanel();
        UpdateMonthText();
        Debug.Log("Juego reiniciado.");
    }

    private void ResetMonthValues()
    {
        moneyAdminister = 50; 
        userSatisfaction = 100f; 

        UpdateMoneyText();
        UpdateReportPanel();
    }

    public void ShowMonthText()
    {
        StartCoroutine(ShowMonthWithFade());
    }

    private IEnumerator ShowMonthWithFade()
    {
        finalImageMonth.gameObject.SetActive(true);
        monthText.text = "Mes " + month;
        monthText.gameObject.SetActive(true);

        Color imageColor = finalImageMonth.color;
        imageColor.a = 0;
        finalImageMonth.color = imageColor;

        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            imageColor.a = t; 
            finalImageMonth.color = imageColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); 

        Color textColor = monthText.color;
        textColor.a = 0; 
        monthText.color = textColor;

        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            textColor.a = t;
            monthText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f); 

        for (float t = 1; t >= 0; t -= Time.deltaTime)
        {
            textColor.a = t; 
            monthText.color = textColor;

            imageColor.a = t; 
            finalImageMonth.color = imageColor;

            yield return null;
        }

        monthText.gameObject.SetActive(false); 
        finalImageMonth.gameObject.SetActive(false); 
    }
}