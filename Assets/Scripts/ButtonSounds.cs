using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : EventTrigger
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        AudioManager.instance.Play("select");
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        AudioManager.instance.Play("confirm");
    }
}
