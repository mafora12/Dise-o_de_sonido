using UnityEngine;
using FMODUnity;

namespace KartGame.KartSystems
{
    public class ArcadeEngineAudio : MonoBehaviour
    {
        public float minRPM = 500;
        public float maxRPM = 2000;
        ArcadeKart arcadeKart;
        private StudioEventEmitter[] emitters;
        private StudioEventEmitter motorEmitter;
        private StudioEventEmitter reverseEmitter;
        private StudioEventEmitter driftEmitter;

        void Awake()
        {
            arcadeKart = GetComponentInParent<ArcadeKart>();
            emitters = GetComponents<StudioEventEmitter>();
            foreach (var e in emitters)
            {
                if (e.EventReference.Path.Contains("mi_motor")) motorEmitter = e;
                if (e.EventReference.Path.Contains("engine_reverse")) reverseEmitter = e;
                if (e.EventReference.Path.Contains("drift")) driftEmitter = e;
            }
        }

        void Update()
        {
            float kartSpeed = arcadeKart != null ? arcadeKart.LocalSpeed() : 0;
            float effectiveRPM = Mathf.Lerp(minRPM, maxRPM, Mathf.Abs(kartSpeed));

            if (motorEmitter != null)
                motorEmitter.SetParameter("RPM", effectiveRPM);

            if (reverseEmitter != null)
                reverseEmitter.SetParameter("RPM", kartSpeed < 0 ? effectiveRPM : 0);

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