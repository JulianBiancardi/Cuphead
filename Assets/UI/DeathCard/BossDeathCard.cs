using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossDeathCard", menuName = "")]
public class BossDeathCard : ScriptableObject
{
    public Sprite bossImage;
    public string phrase;
    public string secondPhrase;

}