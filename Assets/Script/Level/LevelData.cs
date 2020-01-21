using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/CreateNew", order = 1)]
public class LevelData : ScriptableObject
{
    public List<Grid> levelGrids;
}
