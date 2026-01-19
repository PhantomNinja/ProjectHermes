using UnityEngine;

public class EndGoal : MonoBehaviour
{
    private int nextScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneLoader.Instance) {
            if (SceneLoader.Instance.currentSceneIndex > 0)
                nextScene = SceneLoader.Instance.currentSceneIndex + 1;
            else
                nextScene = 2;
        }
        else
            nextScene = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.LoadNextScene(nextScene);
    }
}
