using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateGameObjets : MonoBehaviour
{
    [Header("Animation Settings")]
    public float duration = 0.3f;
    public float squashFactor = 0.8f;
    public float stretchFactor = 1.2f;
    public int vibrato = 2;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        PlayBounce();
    }

    public void PlayBounce()
    {
        // Kill any existing tween to avoid stacking effects
        transform.DOKill();

        Sequence bounce = DOTween.Sequence();

        bounce.Append(transform.DOScale(new Vector3(squashFactor, stretchFactor, 1f), duration * 0.5f).SetEase(Ease.OutQuad))
            .Append(transform.DOScale(new Vector3(stretchFactor, squashFactor, 1f), duration * 0.5f).SetEase(Ease.InOutQuad))
            .Append(transform.DOScale(originalScale, duration * 0.5f).SetEase(Ease.OutBounce)).SetLoops(-1);
    }

    private void Update()
    {
       // OnMouseDown();
    }

    // Optional: Trigger it on click
    private void OnMouseDown()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
            PlayBounce();
    }
   
}
