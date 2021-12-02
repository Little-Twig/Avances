using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Minion Data", menuName = "MinionData")]
public class MinionData : ScriptableObject
{
    public int HP;
    public float distanceRay;
    public float speed;
    public float timer;
}
