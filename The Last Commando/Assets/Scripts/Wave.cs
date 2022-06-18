using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //The array value determines the spawn zone being referenced
    public int[] swarmerEnemyAmount;
    public int[] shooterEnemyAmount;
    public int[] shieldEnemyAmount;
    public int[] artilleryEnemyAmount;

    public int GetTotalEnemysInZone(int zone)
    {
        int total = 0;

        total += swarmerEnemyAmount[zone];
        total += shooterEnemyAmount[zone];
        total += shieldEnemyAmount[zone];
        total += artilleryEnemyAmount[zone];

        return total;
    }

    public int NumberOfZones()
    {
        int number = 0;

        if (swarmerEnemyAmount.Length > number) { number = swarmerEnemyAmount.Length; }
        if (shooterEnemyAmount.Length > number) { number = shooterEnemyAmount.Length; }
        if (shieldEnemyAmount.Length > number) { number = shieldEnemyAmount.Length; }
        if (artilleryEnemyAmount.Length > number) { number = artilleryEnemyAmount.Length; }

        return number;
    }
}
