using UnityEngine;
using System.Collections;

public class LanderFeet : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D hitInfo) {
        if (hitInfo.gameObject.tag == "LanderObjective") {
            var landerObjective = hitInfo.gameObject.GetComponent<LanderObjective>();
            if (landerObjective != null) {
                landerObjective.ActivateLandingPad();
            }
        }
    }
}
