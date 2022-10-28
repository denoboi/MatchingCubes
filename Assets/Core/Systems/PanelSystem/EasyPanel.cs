using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using System;

public enum PanelAnimationTypes
{
    Fade,
    Scale,
}

[RequireComponent(typeof(CanvasGroup))]
public class EasyPanel : MonoBehaviour
{
    #region Getters
    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup { get { return canvasGroup == null ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }
    #endregion

    [SerializeField] private PanelAnimationTypes panelAnimationTypes;

    [Button]
    public virtual void ShowPanel()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
    }

    [Button]
    public virtual void ShowPanelAnimated()
    {
        switch (panelAnimationTypes)
        {
            case PanelAnimationTypes.Fade:
                FadePanel(1, 0.5f, ShowPanel);
                break;
            case PanelAnimationTypes.Scale:
                ScalePanel(true, 0.5f);
                break;
        }
    }

    [Button]
    public virtual void HidePanel()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }

    [Button]
    public virtual void HidePanelAnimated()
    {
        switch (panelAnimationTypes)
        {
            case PanelAnimationTypes.Fade:
                FadePanel(0, 0.5f, HidePanel);
                break;
            case PanelAnimationTypes.Scale:
                ScalePanel(false, 0.5f);
                break;
        }
    }

    private void FadePanel(float value, float duration, Action onComplete = null)
    {
        CanvasGroup.DOFade(value, duration).OnComplete(()=> onComplete());
    }

    private void ScalePanel(bool isShow, float duration)
    {
        if (isShow)
        {
            transform.localScale = Vector3.zero;

            transform.DOScale(Vector3.one, duration).OnStart(ShowPanel);
        }
        else
        {
            transform.DOScale(Vector3.zero, duration).OnComplete(()=> { HidePanel(); transform.localScale = Vector3.one; });
        }
    }
}