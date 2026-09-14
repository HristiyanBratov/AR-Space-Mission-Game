using UnityEngine;

namespace Mission
{
    public class MissionManager : MonoBehaviour
    {
        public enum MissionState
        {
            Waiting,
            Ready,
            MissionActive,
            MissionComplete
        }

        [Header("Mission State")]
        [SerializeField] 
        private MissionState currentState = MissionState.Waiting;

        private bool _planetDetected;
        private bool _astronautDetected;
        private bool _spaceshipDetected;

        public MissionState CurrentState => currentState;
        public bool PlanetObjectiveCompleted { get; private set; }

        public bool AstronautObjectiveCompleted { get; private set; }
        public bool SpaceshipObjectiveCompleted { get; private set; }

        public void SetPlanetDetected(bool detected)
        {
            _planetDetected = detected;
            UpdateMissionState();
        }

        public void SetAstronautDetected(bool detected)
        {
            _astronautDetected = detected;
            UpdateMissionState();
            
            // Astronaut is only an objective after the Planet objective is complete.
            if (detected)
            {
                AstronautFound();
            }
        }

        public void SetSpaceshipDetected(bool detected)
        {
            _spaceshipDetected = detected;
            UpdateMissionState();

            if (detected)
            {
                SpaceshipFound();
            }
        }

        private void UpdateMissionState()
        {
            if (currentState != MissionState.Waiting)
            {
                return;
            }

            if (_planetDetected && _astronautDetected && _spaceshipDetected)
            {
                currentState = MissionState.Ready;

                Debug.Log("Mission is READY! All three targets are detected.");
            }
        }

        public void StartMission()
        {
            if (currentState != MissionState.Ready)
            {
                Debug.Log("Mission cannot start. Mission is not ready.");
                return;
            }

            currentState = MissionState.MissionActive;

            Debug.Log("Mission Started!");
        }
        
        public void PlanetActivated()
        {
            if (currentState != MissionState.MissionActive)
            {
                Debug.Log("Planet cannot be activated. Mission is not active.");
                return;
            }

            if (PlanetObjectiveCompleted)
            {
                return;
            }

            PlanetObjectiveCompleted = true;

            Debug.Log("Planet objective completed!");
            Debug.Log("Next objective: Find the Astronaut.");
        }

        private void AstronautFound()
        {
            if (currentState != MissionState.MissionActive)
            {
                return;
            }

            if (!PlanetObjectiveCompleted)
            {
                return;
            }

            if (AstronautObjectiveCompleted)
            {
                return;
            }
            
            AstronautObjectiveCompleted = true;
            
            Debug.Log("Astronaut objective completed!");
        }

        private void SpaceshipFound()
        {
            if (currentState != MissionState.MissionActive)
            {
                return;
            }

            if (!AstronautObjectiveCompleted)
            {
                return;
            }

            if (SpaceshipObjectiveCompleted)
            {
                return;
            }

            SpaceshipObjectiveCompleted = true;

            Debug.Log("Spaceship completed!");

            CompleteMission();
        }
        
        private void CompleteMission()
        {
            currentState = MissionState.MissionComplete;

            Debug.Log("Mission completed!");
        }
    }
}