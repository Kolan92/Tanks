using System;
using Assets;
using Assets.Scripts.Tank.Controllers;
using Assets.Scripts.Tank.DerivedClasses;
using Assets.Scripts.Tank.Interfaces;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(TankManager))]
public class TankManager {
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector]
    public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector]
    public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector]
    public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector]
    public int m_Wins;                    // The number of wins this player has so far.

    [HideInInspector]
    public bool IsComputercontroled;


    //private TankMovement m_Movement;                        // Reference to tank's movement script, used to disable and enable control.
    //private PlayerTankMovment player;
    //private ComputerTankMovment computer;
    //private ITankMovement tankMovement;
    //private TankShooting m_Shooting;                        // Reference to tank's shooting script, used to disable and enable control.
    //private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.

    private TankController tankController;

    public void Setup() {

        m_ColoredPlayerText = string.Format(
                Constants.ColoredPlayerName, ColorUtility.ToHtmlStringRGB(m_PlayerColor), m_PlayerNumber);

        var renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in renderers) {
            item.material.color = m_PlayerColor;
        }

        tankController = m_Instance.GetComponent<TankController>();
        tankController.Setup(IsComputercontroled, m_PlayerNumber);

        if (IsComputercontroled) {
            m_Instance.tag = "Player";
        }
    }

    public void DisableControl() {
        //tankMovement.enabled = false;
        //m_Shooting.enabled = false;
        tankController.enabled = false;

        //m_CanvasGameObject.SetActive(false);
    }

    public void EnableControl() {
        //tankMovement.enabled = true;
        //m_Shooting.enabled = true;
        tankController.enabled = true;

        //m_CanvasGameObject.SetActive(true);
    }

    public void Reset() {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}
