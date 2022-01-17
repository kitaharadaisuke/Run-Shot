using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] endSelect;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] AudioClip submitSe;
    [SerializeField] AudioClip selectSe;
    [SerializeField] GameObject endPanel;

    GameInput gameInput;
    AudioSource audioSource;

    public float speed = 1.0f;

    int selectNum = 0;
    float time;
    bool isFade = false;
    bool isEnabled = true;
    bool canMove = true;
    bool canSelect = true;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        endPanel.SetActive(false);
    }

    void Update()
    {
        startText.color = GetAlphaColor(startText.color);
        if (gameInput.Menu.Open.triggered && !isFade)
        {
            audioSource.PlayOneShot(submitSe);
            endPanel.SetActive(true);
        }
        else if (gameInput.Menu.Anykey.triggered && isEnabled == true && !isFade)
        {
            audioSource.PlayOneShot(submitSe);
            FadeManager.Instance.LoadScene("SelectScene", 1f);
            isFade = true;
        }

        if (endPanel.activeSelf == false)
        {
            isEnabled = true;
        }
        else
        {
            isEnabled = false;
        }

        if (isEnabled == false)
        {
            for (int i = 0; i < endSelect.Length; i++)
            {
                if (i == selectNum) { endSelect[i].color = GetAlphaColor(endSelect[i].color); }
                else { endSelect[i].color = Color.black; }
            }
            if (gameInput.Menu.Down.triggered)
            {
                if (canMove)
                {
                    if (selectNum < 1) { selectNum++; }
                    else { selectNum = 0; }
                    canMove = false;
                    audioSource.PlayOneShot(selectSe);
                }
            }
            else if (gameInput.Menu.Up.triggered)
            {
                if (canMove)
                {
                    if (selectNum > 0) { selectNum--; }
                    else { selectNum = 1; }
                    canMove = false;
                    audioSource.PlayOneShot(selectSe);
                }
            }
            else { canMove = true; }

            if (gameInput.Menu.Submit.triggered)
            {
                if (canSelect)
                {
                    switch (selectNum)
                    {
                        case 0: //‚¢‚¢‚¦
                            endPanel.SetActive(false);
                            audioSource.PlayOneShot(submitSe);
                            isEnabled = true;
                            break;
                        case 1: //‚Í‚¢
                            audioSource.PlayOneShot(submitSe);
                            QuitGame();
                            break;
                    }
                }
            }
        }
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
