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

            startMissionButton.onClick.AddListener(OnStartMissionClicked);

            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
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
                    missionStatusText.text = "Mission Completed!";
                    startMissionButton.gameObject.SetActive(false);
                    break;
            }
        }

        private void OnStartMissionClicked()
        {
            missionManager.StartMission();
        }

        private void OnDestroy()
        {
            if (startMissionButton != null)
            {
                startMissionButton.onClick.RemoveListener(OnStartMissionClicked);
            }
        }
    }
}