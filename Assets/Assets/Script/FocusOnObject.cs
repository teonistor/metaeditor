using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnObject : MonoBehaviour {

    const float refocusDuration = 0.5f,
                refocusFps = 30f;

    [SerializeField] AnimationCurve refocusSmooth;

    Transform cameraTransform;
    Coroutine focusing;

	void Start () {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        Meshable.OnCentreChange += OnCentreChange;
	}
	
	void Update () {
		
	}

    void OnDisable () {
        Meshable.OnCentreChange -= OnCentreChange;
    }

    void OnCentreChange(string objName, Vector3 newCentre, float distance) {
        print("Centre of " + objName + " has become " + newCentre + " and min distance is " + distance);
        if (focusing != null)
            StopCoroutine(focusing);
        focusing = StartCoroutine(Refocus(newCentre));
    }

    IEnumerator Refocus (Vector3 newCentre) {
        WaitForSeconds wait = new WaitForSeconds(1 / refocusFps);
        Vector3 oldCentre = transform.position;
        Vector3 v = Vector3.zero;

        //for (float t = 0f; t < 1f; t += 1 / refocusFps / refocusDuration) {
        //    transform.position = Vector3.Lerp(oldCentre, newCentre, t);
        //    yield return wait;
        //}

        //for (float t = refocusDuration; t > 0f; t -= refocusDuration / refocusFps) {
        //    transform.position = Vector3.SmoothDamp(transform.position, newCentre, ref v, t, float.PositiveInfinity, refocusDuration / refocusFps);
        //    yield return wait;
        //}

        for (float t = 0f; t < 1f; t += 1 / refocusFps / refocusDuration) {
            transform.position = Vector3.Lerp(oldCentre, newCentre, refocusSmooth.Evaluate(t));
            yield return wait;
        }

        focusing = null;
        transform.position = newCentre;
    }
}
