using UnityEngine;

public class Gizmo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    gameObject.GetComponent<MeshRenderer>().enabled = false;   
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(transform.position, .67f);
    }

}
