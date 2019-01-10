using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnObject : MonoBehaviour {

	void Start () {
        Meshable.OnCentreChange += OnCentreChange;
	}
	
	void Update () {
		
	}

    void OnDisable () {
        Meshable.OnCentreChange -= OnCentreChange;
    }

    void OnCentreChange(string objName, Vector3 newCentre, float distance) {
        print("Centre has become " + newCentre);
    }

    //IEnumerator Refocus () {
    //    WaitForSeconds wait = new WaitForSeconds
    //}
}
