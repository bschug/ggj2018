using UnityEngine;
using System.Collections;

public class EnableOnStoryEvent : MonoBehaviour
{
    public string StoryEvent;

    private void Awake()
    {
        var storyManager = FindObjectOfType<StoryManager>();
        var hasBeenTriggered = storyManager.hasBeenTriggered.Contains(StoryEvent);
        gameObject.SetActive(hasBeenTriggered);
    }
}
