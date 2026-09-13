using Mission;
using UnityEngine;
using Vuforia;

namespace AR
{
    public class ARTargetController : MonoBehaviour
    {
        [Header("AR Object")]
        [SerializeField] private GameObject targetObject;

        [Header("Mission")]
        [SerializeField] private MissionManager missionManager;

        public enum TargetType
        {
            Planet,
            Astronaut,
            Spaceship
        }

        [SerializeField] private TargetType targetType;

        private ObserverBehaviour _observerBehaviour;

        private void Awake()
        {
            _observerBehaviour = GetComponent<ObserverBehaviour>();

            if (_observerBehaviour == null)
            {
                Debug.LogError(
                    $"[{nameof(ARTargetController)}] " +
                    $"No ObserverBehaviour found on {gameObject.name}."
                );

                return;
            }

            _observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        private void OnDestroy()
        {
            if (_observerBehaviour != null)
            {
                _observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
            }
        }

        private void OnTargetStatusChanged(
            ObserverBehaviour behaviour,
            TargetStatus status)
        {
            Debug.Log(
                $"Target: {behaviour.TargetName} | " +
                $"Status: {status.Status} | " +
                $"StatusInfo: {status.StatusInfo}"
            );

            bool isTracked =
                status.Status == Status.TRACKED ||
                status.Status == Status.EXTENDED_TRACKED;

            if (targetObject != null)
            {
                targetObject.SetActive(isTracked);
            }

            UpdateMissionManager(isTracked);
        }

        private void UpdateMissionManager(bool isTracked)
        {
            if (missionManager == null)
            {
                return;
            }

            switch (targetType)
            {
                case TargetType.Planet:
                    missionManager.SetPlanetDetected(isTracked);
                    break;

                case TargetType.Astronaut:
                    missionManager.SetAstronautDetected(isTracked);
                    break;

                case TargetType.Spaceship:
                    missionManager.SetThirdTargetDetected(isTracked);
                    break;
            }
        }
    }
}