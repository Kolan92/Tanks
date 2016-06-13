using UnityEngine;
using System.Collections;

public class TankCameraControler : MonoBehaviour {
    public Camera Camera;

    private void Awake() {
        Camera = GetComponentInChildren<Camera>();
        //enabled = false;
        Camera.gameObject.SetActive(false);
    }
}
