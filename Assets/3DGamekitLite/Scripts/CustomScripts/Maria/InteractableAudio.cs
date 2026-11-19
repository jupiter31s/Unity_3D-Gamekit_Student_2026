using UnityEngine;
using FMOD.Studio;
using FMODUnity;


[CreateAssetMenu(fileName = "InteractableAudio", menuName = "Scriptable Objects/InteractableAudio")]
public class InteractableAudio : ScriptableObject
{
    // Declares a EvenReference that holdsa reference to our specific event
    [SerializeField] private EventReference pickupHealthAudio, pickupKeyAudio, boxDestructableAudio, switchAudio, doorEvent, pressurePadAudio;

    //Declares a method that should play the pickupHealth sound without any 3D-positioning
    public void PickupHealthPlay()
    {
        RuntimeManager.PlayOneShot(pickupHealthAudio);
    }
    
    public void PickupKeyPlay()
    {
        RuntimeManager.PlayOneShot(pickupKeyAudio);
    }
    

    //Declares a method that should play the boxDestructableAudio with 3D-positioning by referencing a Transform position
    public void BoxDestroyPlay(Transform objectTransform)
    {
        RuntimeManager.PlayOneShot(boxDestructableAudio, objectTransform.position);
    }
    
    public void switchAudioPlay(Transform objectTransform)
    {
        RuntimeManager.PlayOneShot(switchAudio, objectTransform.position);
    }


    public EventInstance DoorOpen(EventInstance eventInstance, GameObject obj)
    {
        eventInstance = RuntimeManager.CreateInstance(doorEvent);
        RuntimeManager.AttachInstanceToGameObject(eventInstance, obj, obj.GetComponent<Rigidbody>());
        eventInstance.start();
        return eventInstance;
    }

    public void DoorClose(EventInstance eventInstance)
    {
        eventInstance.keyOff();
        eventInstance.release();
    }
    
    
    
}
