using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DistinctionPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;
    [SerializeField] private float fadeTime;
    [SerializeField] private float animTime;
    [SerializeField] private AnimationCurve animEase;

    private bool pointActive = false;

    public Action onPointerClick;

    private void Awake()
    {
        image.DOFade(0f, 0f);
    }

    public void ActivatePoint()
    {
        pointActive = true;
        PlayAnimation();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (pointActive)
            return;

        onPointerClick?.Invoke();
    }

    private async void PlayAnimation()
    {
        await image.DOFade(1f, fadeTime).AsyncWaitForCompletion();
        image.transform.DOScale(image.transform.localScale * 1.01f, animTime).SetEase(animEase);
    }
}