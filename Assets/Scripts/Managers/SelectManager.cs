using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectManager : MonoBehaviour
{
    [SerializeField] GameObject[] selectPanel;

    GameInput gameInput;
    Image panelImage;

    int selectNum = 0;

    bool canMove = true;
    bool canSelect = true;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < selectPanel.Length; i++)
        {
            if (i == selectNum) { selectPanel[i].gameObject.GetComponent<Image>().color = new Color(255f, 0f, 0f, 255f); }
            else { selectPanel[i].gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f); }
        }
        if (gameInput.Menu.Down.triggered)
        {
            if (canMove)
            {
                if (selectNum < 2) { selectNum++; }
                else { selectNum = 0; }
                canMove = false;
            }
        }

        else if (gameInput.Menu.Up.triggered)
        {
            if (canMove)
            {
                if (selectNum > 0) { selectNum--; }
                else { selectNum = 2; }
                canMove = false;
            }
        }

        else { canMove = true; }

        if (gameInput.Menu.Submit.triggered)
        {
            if (canSelect)
            {
                switch (selectNum)
                {
                    case 0: //ステージ1
                        SceneManager.LoadScene("MainScene");
                        break;
                    case 1: //ステージ2
                        SceneManager.LoadScene("MainScene");
                        break;
                    case 2: //ステージ3
                        SceneManager.LoadScene("MainScene");
                        break;
                }
            }
        }

    }
}
