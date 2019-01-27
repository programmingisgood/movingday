using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoringIndicators : MonoBehaviour
{
    [SerializeField]
    private GameObject scoringIndicatorExample = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowScoring();
        }
    }

    public void ShowScoring()
    {
        StartCoroutine(ShowScoringCoroutine());
    }

    private IEnumerator ShowScoringCoroutine()
    {
        Object[] allMovingItems = Object.FindObjectsOfType<MovingItem>();
        foreach (Object movingItemObj in allMovingItems)
        {
            Transform movingItemTrans = (movingItemObj as MovingItem).transform;
            Vector3 canvasPos = Camera.main.WorldToScreenPoint(movingItemTrans.position);

            GameObject scoringIndicator = Instantiate(scoringIndicatorExample, transform);
            scoringIndicator.SetActive(true);
            scoringIndicator.transform.position = canvasPos;

            scoringIndicator.transform.localScale = Vector3.one * 0.5f;
            scoringIndicator.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            scoringIndicator.transform.DOMove(canvasPos + new Vector3(0, 10f, 0), 1f)
                .OnComplete(() =>
                {
                    scoringIndicator.transform.DOScale(0, 0.5f).SetEase(Ease.InBack)
                        .OnComplete(() =>
                        {
                            Destroy(scoringIndicator);
                        });
                });

            // Spawn a scoring indicator above this object.
            yield return new WaitForSeconds(0.05f);
        }
    }
}
