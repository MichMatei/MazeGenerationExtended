using UnityEngine;

public class ReachedFinish : MonoBehaviour
{
    GameObject reachedEndOptions;

    // Start is called before the first frame update
    void Start()
    {
        reachedEndOptions = GameObject.Find("ReachedEnd");
        reachedEndOptions.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        reachedEndOptions.SetActive(true);
        PlayerMovement.playerMovementInstance.reachedEnd = true;
    }
}
