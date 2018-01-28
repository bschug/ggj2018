using UnityEngine;
using System.Collections;

public class DisableOnStoryEvent : MonoBehaviour
{
    public string StoryEvent;

    void Update()
    {
        var storyManager = FindObjectOfType<StoryManager>();
        var hasBeenTriggered = storyManager.hasBeenTriggered.Contains(StoryEvent);
        if (hasBeenTriggered)
        {
            gameObject.SetActive(false);
        }
    }
}
