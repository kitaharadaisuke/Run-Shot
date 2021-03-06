using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    /*
     * ????
     */

    //?G?̖???
    [SerializeField] string enemyName = "";
    public string EnemyName { get { return enemyName; } }

    //HP
    [SerializeField] int maxHp = 10;
    public int MaxHp { get { return maxHp; } }

    //???x
    [SerializeField] int speed = 1;
    public int Speed { get { return speed; } }

    /*
     * ?U???֘A
     */

    //?ʏ??U??
    [SerializeField] int normalA = 10;
    public int NormalA { get { return normalA; } }

    /*
     * ?{?X
     */

    //?͈͍U??
    [SerializeField] int rangeA = 50;
    public int RangeA { get { return rangeA; } }

    //?З͏d??
    [SerializeField] int powerA = 10;
    public int PowerA { get { return powerA; } }
}
