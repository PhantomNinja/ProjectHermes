using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    [Tooltip("A number between 0 - 1")]
    public float FadedAlphaValue;
    public Renderer[] Renderers;

    private void Start()
    {
        FadeOut();
    }

    public void FadeOut()
    {
        Debug.Log("Fade Out");
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
        Debug.Log("Fade In");
        foreach (Renderer renderer in Renderers)
        {
            Color newColor = renderer.material.color;
            newColor.a = 1; // newAlphaValue is a float between 0 and 1
            renderer.material.color = newColor;
        }

        GetComponent<BoxCollider>().enabled = true;
    }
}
