using UnityEngine;

public class DiffusionManager : MonoBehaviour
{
    [SerializeField] GameObject[] diffusions;

    //この親オブジェクト起動時
    private void OnEnable()
    {
        diffusions[0].SetActive(true);
        diffusions[1].SetActive(true);
        diffusions[2].SetActive(true);
        diffusions[3].SetActive(true);
        diffusions[4].SetActive(true);
    }
}
