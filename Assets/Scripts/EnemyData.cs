using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    /*
     * ‹¤’Ê
     */

    //“G‚Ì–¼Ì
    [SerializeField] string enemyName = "";
    public string EnemyName { get { return enemyName; } }

    //HP
    [SerializeField] int maxHp = 10;
    public int MaxHp { get { return maxHp; } }

    //‘¬“x
    [SerializeField] int speed = 1;
    public int Speed { get { return speed; } }

    /*
     * UŒ‚ŠÖ˜A
     */

    //’ÊíUŒ‚
    [SerializeField] int normalA = 10;
    public int NormalA { get { return normalA; } }

    /*
     * ƒ{ƒX
     */

    //”ÍˆÍUŒ‚
    [SerializeField] int rangeA = 50;
    public int RangeA { get { return rangeA; } }

    //ˆĞ—Íd‹
    [SerializeField] int powerA = 10;
    public int PowerA { get { return powerA; } }
}
