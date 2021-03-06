﻿using System;
using Assets;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(TankManager))]
public class TankManager {

    public Color m_PlayerColor; 
    public Transform m_SpawnPoint;
    [HideInInspector]
    public int m_PlayerNumber;    
    [HideInInspector]
    public string m_ColoredPlayerText;
    [HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public int m_Wins;

    [HideInInspector]
    public bool IsComputercontroled;

    private TankController tankController;
    private TankCameraControler cameraControler;
    
    public void Setup() {

        m_ColoredPlayerText = string.Format(
                Constants.ColoredPlayerName, ColorUtility.ToHtmlStringRGB(m_PlayerColor), m_PlayerNumber);

        var renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in renderers) {
            item.material.color = m_PlayerColor;
        }

        tankController = m_Instance.GetComponent<TankController>();
        tankController.Setup(IsComputercontroled, m_PlayerNumber);

        if (!IsComputercontroled) {
            m_Instance.tag = Constants.Player;
        }
    }

    public void DisableControl() {
        tankController.enabled = false;
    }

    public void EnableControl() {
        tankController.enabled = true;
    }

    public void Reset() {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    public void ActivateCamera() {
        cameraControler = m_Instance.GetComponentInChildren<TankCameraControler>();
        cameraControler.enabled = true;
        cameraControler.Camera.gameObject.SetActive(true);
    }
}
