using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "Cards/Card")]
public class Cards : ScriptableObject
{
    public int health;
    public int damage;
    public Sprite sprite;
    public PreferedBuff buff;
    public Special special;
    

    public enum Special
    {
        none, buff
    }

    public enum PreferedBuff
    {
        health, damage
    }
}
