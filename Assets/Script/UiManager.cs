using System.Collections;
using System.Collections.Generic;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject finishText;

    [SerializeField]
    private GameObject finishButton;

    [SerializeField]
    private TextMeshProUGUI countText; // Change Text to TextMeshProUGUI

    [SerializeField]
    private float fadeDuration = 0.1f;

    [SerializeField]
    private float fadeWaitStartText = 3f;

    [SerializeField]
    private float fadeWaitStartButton = 3.5f;

    void Start()
    {
        // Initialize the panels
        finishText.SetActive(false);
        finishButton.SetActive(false);

        // Subscribe to actions
        PlayerActions.Collect += OnCollectTriggered;
        PlayerActions.Finish += OnFinishTriggered;
        PlayerActions.Death += OnDeathTriggered;
    }

    void OnDestroy()
    {
        // Unsubscribe from actions
        PlayerActions.Collect -= OnCollectTriggered;
        PlayerActions.Death -= OnDeathTriggered;
        PlayerActions.Finish -= OnFinishTriggered;
    }

    void OnCollectTriggered(int amount)
    {
        countText.text = amount.ToString();
    }

    void OnFinishTriggered()
    {
        StartCoroutine(FadeIn(finishText, fadeDuration, fadeWaitStartText));
        StartCoroutine(FadeIn(finishButton, fadeDuration, fadeWaitStartButton));
    }

    void OnDeathTriggered()
    {
        countText.text = "0";
    }

    IEnumerator FadeIn(GameObject obj, float duration, float waitTime)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        float elapsedTime = 0f;

        obj.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
