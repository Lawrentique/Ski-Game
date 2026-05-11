using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private int NextLevelIndex;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        overlay.gameObject.SetActive(true);
        StartCoroutine(FadeOutOverlay());
    }

    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRaceUI;
    }
    private void OnDisable()
    {
        FinishGate.FinishRace -= FinishRaceUI;
    }

    private void FinishRaceUI()
    {
        gameOverPanel.SetActive(true);
    }
    
    private IEnumerator FadeInOverlay()
    {
        while (overlay.alpha < 1.0f)
        {
            overlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    private IEnumerator FadeOutOverlay()
    {
        while (overlay.alpha > 0.0f)
        {
            overlay.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    public void Retry()
    {
        StartCoroutine(RetryCoroutine());
    }

    private IEnumerator RetryCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        StartCoroutine(QuitCoroutine());
    }  
    public IEnumerator QuitCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        Application.Quit();
    }
    
    public void NextRace()
    {
        StartCoroutine(NextLevelCoroutine());
    }

    private IEnumerator NextLevelCoroutine()
    {
        yield return StartCoroutine(FadeInOverlay());
        SceneManager.LoadScene(NextLevelIndex);
    }
    
    void Update()
    {
        
    }
}
