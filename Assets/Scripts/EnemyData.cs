using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    None,
    Vertical,
    Horizontal
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;
    public MoveType moveType;
    public float hp;
    public float maxRange;
    public bool isMove;
}
