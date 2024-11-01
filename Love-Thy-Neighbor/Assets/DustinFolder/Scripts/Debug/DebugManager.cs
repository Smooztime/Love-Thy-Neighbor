using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public WaveManager waveManager;
    public PlayerHealth playerHealth;
    public PointsManager pointsManager;

    private void Update()
    {
        // Just a script to test the UI elements if needed. Each region has code that is commented out so it doesn't interfer with normal gameplay. Uncomment as needed to test, but ensure to re-comment it out when you are done testing whatever it is you need to test. 
        // This is a development only script, do not use it or refer to it with normal scripts

        #region Wave Debugging
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            waveManager.NextWave();
            Debug.Log("Wave Incremented: " + waveManager.currentWave);
        }
        */
        #endregion

        #region Points Debugging
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            int amountToAdd = Random.Range(1, 101); // Random amount between 1 and 100
            pointsManager.AddPoints(amountToAdd);
            Debug.Log("Added Points: " + amountToAdd);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            int amountToSpend = Random.Range(1, 101); // Random amount between 1 and 100
            bool success = pointsManager.GetCurrentPoints() >= amountToSpend;
            pointsManager.SpendPoints(amountToSpend);
            Debug.Log("Attempted to Spend Points: " + amountToSpend);
            Debug.Log(success ? "Transaction Successful!" : "Not enough points!");
        }
        */
        #endregion

        #region Health Debugging
        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            int amountToHeal = Random.Range(1, 21); // Random amount between 1 and 20
            playerHealth.Heal(amountToHeal);
            Debug.Log("Healed Amount: " + amountToHeal);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            int amountToDamage = Random.Range(1, 21); // Random amount between 1 and 20
            playerHealth.TakeDamage(amountToDamage);
            Debug.Log("Damage Taken: " + amountToDamage);
        }
        */
        #endregion
    }
}