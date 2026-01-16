using UnityEngine;

public class FadingPlatform : HermesObject
{
    [Tooltip("A number between 0 - 1")]
    public float FadedAlphaValue;
    public Renderer[] Renderers;

    private MovingObject movingObject;

    public override void StartObject()
    {
        FadeOut();
        movingObject = GetComponent<MovingObject>();
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
        if (movingObject)
        {
            movingObject.objectIsOn = false;
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
        if (movingObject)
        {
            movingObject.objectIsOn = false;
        }
        GetComponent<BoxCollider>().enabled = true;
    }

    public void Toggle()
    {
        if (GetComponent<BoxCollider>().enabled)
            FadeOut();
        else 
            FadeIn();

        if (movingObject)
            movingObject.objectIsOn = GetComponent<BoxCollider>().enabled;
        Debug.Log(movingObject.objectIsOn);
    }

    public override void ResetObject()
    {
        FadeOut();
        base.ResetObject();
    }
}
