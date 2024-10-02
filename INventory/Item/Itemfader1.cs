using UnityEngine;
using DG.Tweening;
//using DG.Tweening.DOTweenModuleSprite; // 正确拼写

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeIN()
    {
        Color targetColor = new Color(1, 1, 1, 1); // 白色
        spriteRenderer.DOColor(targetColor, Setting.fadeDuration); // 目标颜色过渡到白色
    }

    public void FadeOut()
    {
        Color targetColor = new Color(1,1,1,Setting.targetAlpha);
        spriteRenderer.DOColor(targetColor,Setting.fadeDuration);
    }
}
