using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Character Switching
    public GameObject currentCharacter;
    public Queue<GameObject> crowQueue = new Queue<GameObject>();
    public GameObject[] possibleCharacters = new GameObject[3];
    public Transform[] crowPos = new Transform[3];
    public GameObject[] crowSlots = new GameObject[3];
    public GameObject crowPrefab;

    public CinemachineVirtualCamera vCam, vCamMM;
    public PlayerController_Script bigCrowCont;

    public delegate void SendCrow();
    public SendCrow sending;

    public GameObject buttonPanel, startingBarrier;
    private bool inMainMenu;
    private const int two = 2;
    private bool isPaused;
    public GameObject canvasUI;

    public GameObject[] checkpoints = new GameObject[5];

    #region Singleton & Awake
    public static GameManager gMan = null; // should always initilize

    private void Awake()
    {
        if (gMan == null)
        {
            DontDestroyOnLoad(gameObject);
            gMan = this;
        }
        else if (gMan != null)
        {
            Destroy(gameObject); // if its already there destroy it
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        inMainMenu = true;

        for (int i = 0; i < possibleCharacters.Length; i++)
        {
            possibleCharacters[i] = Instantiate(crowPrefab);
            possibleCharacters[i].name = "Crow_" + i;
            var crowCont = possibleCharacters[i].GetComponent<CrowController_Script>();
            crowCont._gMan = this;
            crowCont.myPos = crowPos[i];
            crowCont.crowPosIndex = i;
            crowQueue.Enqueue(possibleCharacters[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentCharacter == null && inMainMenu == false)
        {
            var crow = crowQueue.Dequeue();
            currentCharacter = crow;
            var crowCont = crow.GetComponent<CrowController_Script>();
            crowCont.isFollowing = false;
            bigCrowCont.enabled = false;
            sending.Invoke();
            CharacterSwap();
        }     

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void CharacterSwap()
    {
        VCamTargets(currentCharacter.transform);
    }

    public void EmptyCurrentCharacter()
    {
        currentCharacter = null;

        VCamTargets(bigCrowCont.transform);
        bigCrowCont.enabled = true;
    }

    public void RespawnAtLastCheckpoint()
    {
        bigCrowCont.gameObject.transform.position = checkpoints[0].transform.position;
    }

    private void VCamTargets(Transform currentChar)
    {
        vCam.LookAt = currentChar;
        vCam.Follow = currentChar;
    }

    public void PlayGame()
    {
        // Disable Buttons and UI
        Debug.Log("Starting Game...");
        buttonPanel.GetComponent<Animator>().SetBool("StartGame", true);
        StartCoroutine(DelayGameState());
    }

    IEnumerator DelayGameState()
    {
        yield return new WaitForSeconds(two);
        buttonPanel.SetActive(false);
        vCam.enabled = true;
        vCamMM.enabled = false;
        inMainMenu = false;
        startingBarrier.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            canvasUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            canvasUI.SetActive(false);
        }
    }
}
