using Mission;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MissionUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] 
        private MissionManager missionManager;
        
        [SerializeField] 
        private TMP_Text missionStatusText;
        
        [SerializeField] 
        private Button startMissionButton;
        
        [SerializeField] 
        private GameObject missionPanel;
        
        [SerializeField] 
        private GameObject missionCompletePanel;
        
        [SerializeField] 
        private Button playAgainButton;

        private void Start()
        {
            if (missionManager == null)
            {
                Debug.LogError("MissionUI: Mission Manager is not assigned.");
                return;
            }

            if (missionStatusText == null)
            {
                Debug.LogError("MissionUI: Mission Status Text is not assigned.");
                return;
            }

            if (startMissionButton == null)
            {
                Debug.LogError("MissionUI: Start Mission Button is not assigned.");
                return;
            }
            
            if (missionPanel == null)
            {
                Debug.LogError("MissionUI: Mission Panel is not assigned.");
                return;
            }

            if (missionCompletePanel == null)
            {
                Debug.LogError("MissionUI: Mission Complete Panel is not assigned.");
                return;
            }

            if (playAgainButton == null)
            {
                Debug.LogError("MissionUI: Play Again Button is not assigned.");
                return;
            }
            
            startMissionButton.onClick.AddListener(OnStartMissionClicked);
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);

            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            missionPanel.SetActive(true);
            missionCompletePanel.SetActive(false);
            
            switch (missionManager.CurrentState)
            {
                case MissionManager.MissionState.Waiting:
                    missionStatusText.text = "Find all three targets!";
                    startMissionButton.gameObject.SetActive(false);
                    break;

                case MissionManager.MissionState.Ready:
                    missionStatusText.text = "All targets detected!";
                    startMissionButton.gameObject.SetActive(true);
                    break;

                case MissionManager.MissionState.MissionActive:

                    if (missionManager.SpaceshipObjectiveCompleted)
                    {
                        missionStatusText.text = "Objective Complete!";
                    }
                    else if (missionManager.AstronautObjectiveCompleted)
                    {
                        missionStatusText.text = "Objective: Find the Mission Object.";
                    }
                    else if (missionManager.PlanetObjectiveCompleted)
                    {
                        missionStatusText.text = "Objective: Find the Astronaut.";
                    }
                    else
                    {
                        missionStatusText.text = "Objective: Activate the Planet.";
                    }

                    startMissionButton.gameObject.SetActive(false);
                    break;

                case MissionManager.MissionState.MissionComplete:
                    missionPanel.SetActive(false);
                    missionCompletePanel.SetActive(true);
                    break;
            }
        }

        private void OnStartMissionClicked()
        {
            missionManager.StartMission();
        }
        
        private void OnPlayAgainClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDestroy()
        {
            if (startMissionButton != null)
            {
                startMissionButton.onClick.RemoveListener(OnStartMissionClicked);
            }
            
            if (playAgainButton != null)
            {
                playAgainButton.onClick.RemoveListener(OnPlayAgainClicked);
            }
        }
    }
}