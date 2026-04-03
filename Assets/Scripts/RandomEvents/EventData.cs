using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewEvent", menuName = "I'm Not Insane/Event")]
public class EventData : ScriptableObject
{
    public string eventTitle = "Event Title";
    public string eventDescription = "What happens...";

    public List<Choice> choices = new List<Choice>();
}

[System.Serializable]
public class Choice
{
    public string choiceText = "Choice text here";
    public int stressChange = 0;
    public int trustChange = 0;

    public string positiveOutcome = "Good result description...";
    public string negativeOutcome = "Bad result description...";
    public float positiveChance = 0.5f;   // Chance to get positive outcome
}