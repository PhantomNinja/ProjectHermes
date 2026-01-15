using UnityEngine;

public class FadingPlatform : HermesObject
{
    [Tooltip("A number between 0 - 1")]
    public float FadedAlphaValue;
    public Renderer[] Renderers;

    public override void StartObject()
    {
        FadeOut();
        base.StartObject();
    }

    public void FadeOut()
    {
        foreach (Renderer renderer in Renderers)
        {
            Color newColor = renderer.material.color;
            newColor.a = FadedAlphaValue; // newAlphaValue is a float between 0 and 1
            renderer.material.color = newColor;
        }
        GetComponent<BoxCollider>().enabled = false;
    }

    public void FadeIn()
    {
        foreach (Renderer renderer in Renderers)
        {
            Color newColor = renderer.material.color;
            newColor.a = 1; // newAlphaValue is a float between 0 and 1
            renderer.material.color = newColor;
        }

        GetComponent<BoxCollider>().enabled = true;
    }

    public void Toggle()
    {
        if (GetComponent<BoxCollider>().enabled)
            FadeOut();
        else FadeIn();
    }

    public override void ResetObject()
    {
        FadeOut();
        base.ResetObject();
    }
}
