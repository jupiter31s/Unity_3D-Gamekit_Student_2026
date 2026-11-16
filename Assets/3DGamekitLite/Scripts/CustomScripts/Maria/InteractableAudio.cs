using UnityEngine;
using FMOD.Studio;
using FMODUnity;


[CreateAssetMenu(fileName = "InteractableAudio", menuName = "Scriptable Objects/InteractableAudio")]
public class InteractableAudio : ScriptableObject
{
    // Declares a EvenReference that holdsa reference to our specific event
    [SerializeField] private EventReference pickupHealthAudio, pickupKeyAudio, boxDestructableAudio, switchAudio, doorAudio, pressurePadAudio;

    //Declares a method that should play the pickupHealth sound without any 3D-positioning
    public void PickupHealthPlay()
    {
        RuntimeManager.PlayOneShot(pickupHealthAudio);
    }
}
