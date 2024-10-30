using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveCounterText;
    public int currentWave;

    private void Start()
    {
        currentWave = 1;
        UpdateWaveCounterUI();
    }

    public void NextWave()
    {
        currentWave++;
        UpdateWaveCounterUI();
    }

    private void UpdateWaveCounterUI()
    {
        waveCounterText.text = "Wave: " + currentWave;
    }
}