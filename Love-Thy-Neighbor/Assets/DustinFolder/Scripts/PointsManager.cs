using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    private int currentPoints;

    private void Start()
    {
        currentPoints = 0;
        UpdatePointsUI();
    }

    public void AddPoints(int amount)
    {
        currentPoints += amount;
        UpdatePointsUI();
    }

    public void SpendPoints(int amount)
    {
        if (currentPoints >= amount)
        {
            currentPoints -= amount;
            UpdatePointsUI();
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }

    private void UpdatePointsUI()
    {
        pointsText.text = "Points: " + currentPoints;
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }
}