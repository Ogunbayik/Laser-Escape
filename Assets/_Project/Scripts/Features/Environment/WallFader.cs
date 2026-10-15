using DG.Tweening;
using UnityEngine;

public class WallFader : MonoBehaviour
{
    private Renderer wallRenderer;
    private MaterialPropertyBlock mpb;

    private Color originalColor = Color.white;
    private Tween fadeTween;

    private void Awake()
    {
        wallRenderer = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();

        if (wallRenderer != null && wallRenderer.sharedMaterial != null)
        {
            if (wallRenderer.sharedMaterial.HasProperty(GameConstant.ShaderProperties.BaseColorID))
            {
                originalColor = wallRenderer.sharedMaterial.GetColor(GameConstant.ShaderProperties.BaseColorID);
            }
        }
    }

    public void FadeOut(float targetAlpha, float duration)
    {
        if (wallRenderer == null) return;

        fadeTween?.Kill();

        wallRenderer.GetPropertyBlock(mpb);
        Color currentColor = mpb.HasProperty(GameConstant.ShaderProperties.BaseColorID) ? mpb.GetColor(GameConstant.ShaderProperties.BaseColorID) : originalColor;

        fadeTween = DOTween.To(() => currentColor.a, x =>
        {
            currentColor.a = x;
            mpb.SetColor(GameConstant.ShaderProperties.BaseColorID, currentColor);
            wallRenderer.SetPropertyBlock(mpb);
        }, targetAlpha, duration);
    }

    public void FadeIn(float duration)
    {
        if (wallRenderer == null) return;

        fadeTween?.Kill();

        wallRenderer.GetPropertyBlock(mpb);
        Color currentColor = mpb.HasProperty(GameConstant.ShaderProperties.BaseColorID) ? mpb.GetColor(GameConstant.ShaderProperties.BaseColorID) : originalColor;

        fadeTween = DOTween.To(() => currentColor.a, x =>
        {
            currentColor.a = x;
            mpb.SetColor(GameConstant.ShaderProperties.BaseColorID, currentColor);
            wallRenderer.SetPropertyBlock(mpb);
        }, originalColor.a, duration).OnComplete(() =>
        {
            wallRenderer.SetPropertyBlock(null);
        });
    }
}
