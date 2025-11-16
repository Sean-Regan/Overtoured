using UnityEngine;

public class CrowdToggle : MonoBehaviour
{
    public GameObject lowCrowd;
    public GameObject highCrowd;

    private bool crowded = false;

    public void ToggleCrowd()
    {
        Debug.Log("ToggleCrowd() called!");

        // Safety in case refs aren’t set
        if (lowCrowd == null || highCrowd == null)
        {
            Debug.LogWarning("lowCrowd or highCrowd is NOT assigned in the inspector.");
            return;
        }

        crowded = !crowded;

        lowCrowd.SetActive(!crowded);
        highCrowd.SetActive(crowded);

        Debug.Log("Crowd mode: " + (crowded ? "HIGH" : "LOW"));
    }
}
