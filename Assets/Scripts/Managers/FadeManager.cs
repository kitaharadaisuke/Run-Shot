using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private static Canvas canvas;
    private static Image image;
    private static Text loadingText;

    private static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null) { Init(); }
            return instance;
        }
    }

    IEnumerator fadeCoroutine = null;
    AsyncOperation async;

    private FadeManager() { }

    private void Update()
    {

    }

    private static void Init()
    {
        // Canvas�쐬
        GameObject canvasObject = new GameObject("CanvasFade");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        // Image�쐬
        image = new GameObject("ImageFade").AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);

        // ��ʒ������A���J�[�Ƃ��AImage�̃T�C�Y���X�N���[���T�C�Y�ɍ��킹��
        image.rectTransform.anchoredPosition = Vector3.zero;
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // Text�쐬(Now Loading)
        loadingText = new GameObject("LoadingText").AddComponent<Text>();
        loadingText.transform.SetParent(canvas.transform, false);
        // �t�H���g�A���A�����T�C�Y�A���������Ȃǂ��Z�b�g
        loadingText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        loadingText.rectTransform.pivot = new Vector2(1, 0);
        loadingText.rectTransform.anchorMax = new Vector2(1, 0);
        loadingText.rectTransform.anchorMin = new Vector2(1, 0);
        loadingText.rectTransform.sizeDelta = new Vector2(500, 200);
        loadingText.fontSize = 60;
        loadingText.alignment = TextAnchor.MiddleLeft;
        loadingText.color = Color.white;
        loadingText.enabled = false;    // �t�F�[�h�I���܂Ŕ�\��

        // �J�ڐ�V�[���ł��I�u�W�F�N�g��j�����Ȃ�
        DontDestroyOnLoad(canvas.gameObject);

        // �V���O���g���I�u�W�F�N�g��ێ�
        canvasObject.AddComponent<FadeManager>();
        instance = canvasObject.GetComponent<FadeManager>();
    }

    // �t�F�[�h�t���V�[���J�ڂ��s��
    public void LoadScene(string sceneName, float interval = 1f)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = null;

        fadeCoroutine = Fade(sceneName, interval);
        StartCoroutine(fadeCoroutine);
    }
    private IEnumerator Fade(string sceneName, float interval)
    {
        float time = 0f;
        canvas.enabled = true;

        // �t�F�[�h�A�E�g
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            image.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // �V�[���̓ǂݍ��݂��J�n
        async = SceneManager.LoadSceneAsync(sceneName);
        // ���[�h���������Ă������V�[���̃A�N�e�B�u�������Ȃ�
        async.allowSceneActivation = false;

        time = 0;
        loadingText.enabled = true;
        while (async.progress < 0.9f) // 1.0��0.9f�Ŏg����悤�ɂȂ� !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!<===========================--
        {
            time += Time.deltaTime;
            // 0.3�b���Ƃɕ\���ؑ�
            if (time < 0.3f) { loadingText.text = "Now Loading"; }              // �Q�[���̕��͋C��b�������ĕς���
            else if (time < 0.6f) { loadingText.text = "Now Loading."; }        // Now Loading�͂Ƃ肠�����ύX������.
            else if (time < 0.9f) { loadingText.text = "Now Loading.."; }       //
            else if (time < 1.2f) { loadingText.text = "Now Loading..."; }      //
            else { time = 0f; }
            yield return null;
        }
        // ���[�h������A0.5�b�҂��Ă���V�[������
        yield return new WaitForSeconds(0.5f);
        loadingText.enabled = false;
        async.allowSceneActivation = true;

        // �V�[���񓯊����[�h
        yield return SceneManager.LoadSceneAsync(sceneName);

        // �t�F�[�h�C��
        time = 0f;
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            image.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // �`����X�V���Ȃ�
        canvas.enabled = false;
    }
}