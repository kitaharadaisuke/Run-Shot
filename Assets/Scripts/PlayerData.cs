using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    //HP
    [SerializeField] int maxHp = 1000;
    public int MaxHP { get { return maxHp; } }

    //ビームゲージ
    [SerializeField] int bG = 100;
    public int BG { get { return bG; } }

    //スタミナ
    [SerializeField] int stamina = 100;
    public int Stamina { get { return stamina; } }

    //速度
    [SerializeField] int speed = 1;
    public int Speed { get { return speed; } }

    //ダッシュ
    [SerializeField] int dash = 2;
    public int Dash { get { return dash; } }

    /*
     * 攻撃関連
     */

    //通常弾
    [SerializeField] int normalA = 5;
    public int NormalA { get { return normalA; } }

    //ビーム1
    [SerializeField] int specialA1 = 30;
    public int SpecialA1 { get { return specialA1; } }

    //ビーム2
    [SerializeField] int specialA2 = 10;
    public int SpecialA2 { get { return specialA2; } }

    //ビーム3
    [SerializeField] int specialA3 = 5;
    public int SpecialA3 { get { return specialA3; } }
}
