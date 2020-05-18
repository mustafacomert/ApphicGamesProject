using UnityEngine;
using System.Collections;
using System.IO;

public class RotationScript : MonoBehaviour
{
	public bool rotationEnabled = false;

	public float rotationRate = 0f;

    private Transform transformCached;

	void Start() {
	    transformCached = transform;
	}

	void Update() {
		if (rotationEnabled) {
            if (transformCached.GetComponent<Renderer>() != null) {
                transformCached.Rotate(0, 0, rotationRate * Time.deltaTime, Space.Self);
            }
		}
	}

}