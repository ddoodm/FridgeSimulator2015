using UnityEngine;
using System.Collections;

public class PlatformDestroyTrigger : MonoBehaviour
{
    /// <summary>
    /// Previous and current platform objects (per frame)
    /// </summary>
    private GameObject pPlatform, cPlatform;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void dropPlatform(GameObject plat)
    {
        // Obtain the "dropper controller" for the old object, if it has one
        PlatformDropper dropper = pPlatform.GetComponent<PlatformDropper>();

        if (dropper == null)
            return;

        dropper.drop();
    }

    void Update()
    {
        cPlatform = playerController.objectUnderPlayer;

        // The player is no longer on the (previous) platform, so tell it to go away
        if (cPlatform != pPlatform && pPlatform != null)
        {
            // Check whether we're over a gate (or similar)
            bool cPlatformIsInvalid = true;
            if (cPlatform == null)
                cPlatformIsInvalid = false;
            else
                foreach (string tag in playerController.platformTags)
                    if (tag == cPlatform.tag)
                        cPlatformIsInvalid = false;

            if (!cPlatformIsInvalid)
            {
                Debug.Log(string.Format("PlatformDestroyTrigger - pPlatform:{0}, cPlatform:{1}", pPlatform, cPlatform));
                dropPlatform(pPlatform);
            }
        }

        pPlatform = cPlatform;
    }
}