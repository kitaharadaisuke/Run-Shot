using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    //HP
    [SerializeField] int maxHp = 1000;
    public int MaxHP { get { return maxHp; } }

    //�r�[���Q�[�W
    [SerializeField] float bG = 100;
    public float BG { get { return bG; } }

    //�X�^�~�i
    [SerializeField] float stamina = 100;
    public float Stamina { get { return stamina; } }

    //���x
    [SerializeField] int speed = 1;
    public int Speed { get { return speed; } }

    //�_�b�V��
    [SerializeField] int dash = 2;
    public int Dash { get { return dash; } }

    /*
     * �U���֘A
     */

    //�ʏ�e
    [SerializeField] int normalA = 5;
    public int NormalA { get { return normalA; } }

    //�r�[��1
    [SerializeField] int specialA1 = 30;
    public int SpecialA1 { get { return specialA1; } }

    //�r�[��2
    [SerializeField] int specialA2 = 10;
    public int SpecialA2 { get { return specialA2; } }

    //�r�[��3
    [SerializeField] int specialA3 = 5;
    public int SpecialA3 { get { return specialA3; } }
}