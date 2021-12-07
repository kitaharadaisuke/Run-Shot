using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefab = null;
    int stageNum;
    private void Awake()
    {
        stageNum = SelectManager.GetStageNum();
        GameObject stage = Instantiate(stagePrefab[stageNum]) as GameObject;
        stage.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
