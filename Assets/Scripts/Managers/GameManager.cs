using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI[] overSelect;
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] GameObject OverPanel;

    GameInput gameInput;
    
    public int conbo = 0;

    int playerHp;
    int overSelectNum = 0;

    bool overCanMove = true;
    bool overCanSelect = true;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
        OverPanel.SetActive(false);
    }

    void Update()
    {
        playerHp = player.hp;
        //ゲームオーバー時の処理
        if (playerHp < 0)
        {
            player.enabled = false;
            OverPanel.SetActive(true);
            GameOverMenu();
        }

        //コンボ数表示
        conboText.text = conbo.ToString("0");
    }

    void GameOverMenu()
    {
        for (int i = 0; i < overSelect.Length; i++)
        {
            if (i == overSelectNum) { overSelect[i].alpha = 1f; }
            else { overSelect[i].alpha = 0.5f; }
        }
        if (gameInput.Menu.Up.triggered)
        {
            if (overCanMove)
            {
                if (overSelectNum < 1) { overSelectNum++; }
                else { overSelectNum = 0; }
                overCanMove = false;
            }
        }
        else if (gameInput.Menu.Down.triggered)
        {
            if (overCanMove)
            {
                if (overSelectNum > 0) { overSelectNum--; }
                else { overSelectNum = 1; }
                overCanMove = false;
            }
        }
        else { overCanMove = true; }

        if (gameInput.Menu.Submit.triggered)
        {
            if (overCanSelect)
            {
                switch (overSelectNum)
                {
                    case 0: //リトライ
                        SceneManager.LoadScene("MainScene");
                        break;
                    case 1: //セレクトシーンに戻る
                        //SceneManager.LoadScene("SelectScene");
                        break;
                }
            }
        }
    }
}
