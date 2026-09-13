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
        [SerializeField] private MissionState currentState = MissionState.Waiting;

        private bool _planetDetected;
        private bool _astronautDetected;
        private bool _thirdTargetDetected;

        public MissionState CurrentState => currentState;

        public void SetPlanetDetected(bool detected)
        {
            _planetDetected = detected;
            UpdateMissionState();
        }

        public void SetAstronautDetected(bool detected)
        {
            _astronautDetected = detected;
            UpdateMissionState();
        }

        public void SetThirdTargetDetected(bool detected)
        {
            _thirdTargetDetected = detected;
            UpdateMissionState();
        }

        private void UpdateMissionState()
        {
            if (currentState != MissionState.Waiting)
            {
                return;
            }

            if (_planetDetected &&
                _astronautDetected &&
                _thirdTargetDetected)
            {
                currentState = MissionState.Ready;

                Debug.Log("Mission is READY. All three targets detected.");
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

            Debug.Log("Mission started!");
        }
    }
}