using System;
using UnityEngine;
using UnityEngine.Rendering;
using FMOD.Studio;
using FMODUnity;


namespace Gamekit3D.GameCommands
{
    public abstract class SimpleTransformer : GameCommandHandler
    {
        public enum LoopType
        {
            Once,
            PingPong,
            Repeat
        }

        public LoopType loopType;

        public float duration = 1;
        public AnimationCurve accelCurve;

        public bool activate = false;
        public SendGameCommand OnStartCommand, OnStopCommand;
        

        [Range(0, 1)]
        public float previewPosition;
        float time = 0f;
        float position = 0f;
        float direction = 1f;

        protected Platform m_Platform;

        private EventInstance eventInstance;
        public InteractableAudio interactableAudio;

        private void Update()
        {
            if (interactableAudio != null && Input.GetKeyDown(KeyCode.Alpha0))
            {
                eventInstance = interactableAudio.DoorOpen(eventInstance, gameObject);
            }

            if (interactableAudio != null && Input.GetKeyDown(KeyCode.Alpha1))
            {
                interactableAudio.DoorClose(eventInstance);
            }
        }

        
        
        protected override void Awake()
        {
            base.Awake();

            m_Platform = GetComponentInChildren<Platform>();
        }

        public override void PerformInteraction()
        {
            // Door is being opened
            Debug.Log("Is the door opem");
            
            activate = true;
            if (OnStartCommand != null) OnStartCommand.Send();
        }

        public void FixedUpdate()
        {
            if (activate)
            {
                time = time + (direction * Time.deltaTime / duration);
                switch (loopType)
                {
                    case LoopType.Once:
                        LoopOnce();
                        break;
                    case LoopType.PingPong:
                        LoopPingPong();
                        break;
                    case LoopType.Repeat:
                        LoopRepeat();
                        break;
                }
                PerformTransform(position);
            }
        }

        public virtual void PerformTransform(float position)
        {

        }

        void LoopPingPong()
        {
            position = Mathf.PingPong(time, 1f);
            
        }

        void LoopRepeat()
        {
            position = Mathf.Repeat(time, 1f);
        }

        
        void LoopOnce()
        {
            
            position = Mathf.Clamp01(time);
            if (position >= 1)
            {
                enabled = false;
                if (OnStopCommand != null) OnStopCommand.Send();
                direction *= -1;
            }
        }
    }
}
