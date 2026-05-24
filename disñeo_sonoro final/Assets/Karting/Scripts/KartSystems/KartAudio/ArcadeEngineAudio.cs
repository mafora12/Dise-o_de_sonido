using UnityEngine;
using FMODUnity;

namespace KartGame.KartSystems
{
    public class ArcadeEngineAudio : MonoBehaviour
    {
        public float minRPM = 500;
        public float maxRPM = 2000;
        ArcadeKart arcadeKart;

        [Header("FMOD Events")]
        public StudioEventEmitter idleEmitter;
        public StudioEventEmitter runningEmitter;
        public StudioEventEmitter reverseEmitter;
        public StudioEventEmitter driftEmitter;

        void Awake()
        {
            arcadeKart = GetComponentInParent<ArcadeKart>();
        }

        void Update()
        {
            float kartSpeed = arcadeKart != null ? arcadeKart.LocalSpeed() : 0;

            if (kartSpeed < 0)
            {
                idleEmitter?.Stop();
                runningEmitter?.Stop();
                reverseEmitter?.Play();
            }
            else
            {
                reverseEmitter?.Stop();
                if (kartSpeed < 0.1f)
                {
                    runningEmitter?.Stop();
                    idleEmitter?.Play();
                }
                else
                {
                    idleEmitter?.Stop();
                    runningEmitter?.Play();
                }
            }

            if (driftEmitter != null && arcadeKart != null)
            {
                if (arcadeKart.IsDrifting && arcadeKart.GroundPercent > 0)
                    driftEmitter.Play();
                else
                    driftEmitter.Stop();
            }
        }
    }
}