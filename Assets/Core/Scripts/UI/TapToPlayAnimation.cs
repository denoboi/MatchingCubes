using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayAnimation : MonoBehaviour
{
    private void Awake()
    {
        transform.DOPunchScale(Vector2.one * 0.1f, 1f, 1, 1).SetLoops(-1);
    }
}
