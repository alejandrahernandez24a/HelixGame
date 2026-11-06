using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Level
{
    [Range(1, 11)]
    public int perCount = 11;

    [Range(0, 11)]
    public int deathParttCount = 1;
}

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
     public Color stageBackgroundColor = Color.white;
     public Color stageLevelPartColor = Color.white;
     public Color stageBallColor = Color.white;

     public List<Level> levels = new List<Level>();

}
