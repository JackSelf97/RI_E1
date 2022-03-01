using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
    public Canvas canvasDeath;
    public Text timesDied, crowsLost;
    public int timesDiedNo, crowsLostNo;

    // Checkpoints
    public GameObject[] checkpoints = new GameObject[4];
    public GameObject[] barriers = new GameObject[2];
    public GameObject currentCheckpoint;
    public bool isAlive;

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
        isAlive = true;

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
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.E) && currentCharacter == null && inMainMenu == false)
            {
                bigCrowCont.HaltMovement();
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

        if (currentCharacter == null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LoadLastCheckpoint();
            }
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
        FindObjectOfType<AudioManager>().PlaySound("Damage");
        Debug.Log("I should be called once");
        canvasDeath.enabled = true;
        isAlive = false;
        bigCrowCont.gameObject.transform.position = currentCheckpoint.transform.position;
        timesDied.text = "Times Died: " + timesDiedNo;
        crowsLost.text = "Crows Lost: " + crowsLostNo;
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

    public void LoadLastCheckpoint()
    {
        if (currentCheckpoint == null)
        {
            if (isPaused)
                PauseGame();

            return;
        }

        if (!isPaused || !isAlive)
        {
            canvasDeath.enabled = false;
            bigCrowCont.gameObject.transform.position = currentCheckpoint.transform.position;
            isAlive = true;
            return;
        }

        ClassicRespawn();
    }

    public void Restart()
    {
        if (inMainMenu)
        {
            PauseGame();
            return;
        }

        currentCheckpoint = checkpoints[0]; // first checkpoint
        timesDiedNo = 0;
        crowsLostNo = 0;
        ClassicRespawn();
    }

    public void VictoryState()
    {
        canvasDeath.enabled = true;
        canvasDeath.GetComponentInChildren<VictoryText_Script>().enabled = true;
    }

    private void ClassicRespawn()
    {
        PauseGame();
        bigCrowCont.gameObject.transform.position = currentCheckpoint.transform.position;
    }

    public void PlayHoverUIButtonSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UI_Hover");
    }
}
