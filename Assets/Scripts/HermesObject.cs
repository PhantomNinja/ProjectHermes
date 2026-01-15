using UnityEngine;

public class HermesObject : MonoBehaviour
{
    [Tooltip("The area the object is in")]
    public int CheckArea;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void StartObject()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    public virtual void UpdateObject()
    {
        
    }

    public virtual void ResetObject()
    {
        transform.position = startPos;
    }
    public virtual void FixedUpdateObject()
    {

    }
}
