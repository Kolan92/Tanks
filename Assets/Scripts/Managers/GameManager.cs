using System;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Tank.Enums;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Button _pvpGame;

    [SerializeField]
    private Button _pvcGame;

    [SerializeField]
    private Button _quit;

    public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
    public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
    public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
    public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
    public Canvas OptionsCanvas;
    public GameObject m_TankPrefab;             // Reference to the prefab the players will control.
    public TankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks.


    private int m_RoundNumber;                  // Which round the game is currently on.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.

    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private TankManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
    private TankManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.
    private bool isVsComputerGame;


    public void Start() {
        AttachEvents();
        OptionsCanvas.enabled = true;
    }

    private void AttachEvents() {
        _pvpGame.onClick.AddListener(StartPvpGame);
        _pvcGame.onClick.AddListener(StartPvcGame);
        _quit.onClick.AddListener(QuitGame);
    }

    private void DisableEvents() {
        _pvpGame.onClick.RemoveAllListeners();
        _pvcGame.onClick.RemoveAllListeners();
        _quit.onClick.RemoveAllListeners();
    }

    private void StartPvpGame() {
        isVsComputerGame = false;
        Setup();
        Debug.Log("PVPStartGame!");
    }

    private void StartPvcGame() {
        isVsComputerGame = true;
        Setup();
        Debug.Log("PVCStartGame!");
    }

    private void QuitGame() {
        Application.Quit();
    }

    public void Setup() {
        DisableEvents();
        OptionsCanvas.enabled = false;
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();
        StartCoroutine(GameLoop());
    }

    private void SpawnAllTanks() {

        var tanksCount = m_Tanks.Length;
        for (var i = 0; i < tanksCount; i++) {
            m_Tanks[i].m_Instance = Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            
            if (isVsComputerGame && i == tanksCount-1) {
                m_Tanks[i].IsComputercontroled = true;
            }
            if (isVsComputerGame && i==0){
                m_Tanks[i].ActivateCamera();
            }
            m_Tanks[i].Setup();
        }
    }

    private void SetCameraTargets() {
        var targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++) {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }
        m_CameraControl.m_Targets = targets;
    }

    private IEnumerator GameLoop() {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null) {
            Application.LoadLevel(Application.loadedLevel);
        }
        else {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting() {
        ResetAllTanks();
        DisableTankControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;
        OptionsCanvas.enabled = false;

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying() {
        EnableTankControl();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft()) {
            yield return null;
        }
    }

    private IEnumerator RoundEnding() {
        Dispatcher.Dispatch(GameEventEnum.RoundEnded);
        DisableTankControl();
        m_RoundWinner = null;
        m_RoundWinner = GetRoundWinner();
        if (m_RoundWinner != null)
            m_RoundWinner.m_Wins++;

        m_GameWinner = GetGameWinner();
        var message = EndMessage();
        m_MessageText.text = message;
        yield return m_EndWait;
    }

    private bool OneTankLeft() {
        var numTanksLeft = 0;
        foreach (var tank in m_Tanks) {
            if (tank.m_Instance.activeSelf)
                numTanksLeft++;
        }
        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner() {
        foreach (var tank in m_Tanks) {
            if (tank.m_Instance.activeSelf)
                return tank;
        }
        return null;
    }

    private TankManager GetGameWinner() {
        foreach (var tank in m_Tanks) {
            if (tank.m_Wins >= m_NumRoundsToWin)
                return tank;
        }
        return null;
    }

    private string EndMessage() {
        var message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        foreach (var tank in m_Tanks) {
            message += tank.m_ColoredPlayerText + ": " + tank.m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }

    private void ResetAllTanks() {
        foreach (var tank in m_Tanks) {
            tank.Reset();
        }
    }

    private void EnableTankControl() {
        foreach (var tank in m_Tanks) {
            tank.EnableControl();
        }
    }

    private void DisableTankControl() {
        foreach (var tank in m_Tanks) {
            tank.DisableControl();
        }
    }
}