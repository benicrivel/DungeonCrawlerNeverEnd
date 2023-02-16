using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon Stuff/New Character")]
public class Character : ScriptableObject
{
    public string charName;
    public int healthStat;
    public int strenghtStat;
    public int speedStat;
}
