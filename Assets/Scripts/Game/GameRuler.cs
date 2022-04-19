using UnityEngine;
using UnityEngine.Events;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Roulette roulette1;
    [SerializeField] private Arrow arrow1;
    [SerializeField] private Roulette roulette2;
    [SerializeField] private Arrow arrow2;

    
    [SerializeField] private ScoreCounter scoreCounter;
    
    

    public UnityEvent arrowCoincidedEvent;
    public UnityEvent endSpinsEvent;
    
    public UnityEvent addedScoreEvent;
    public UnityEvent remowedScoreEvent;

    private int endWorkSlots = 0;
    private bool addScore = true;

    public void SetEndSlot()
    {
        endWorkSlots++;
        GetResults();
    }

    private void OnEnable()
    {
        roulette1.endRotateEvent?.AddListener(SetEndSlot);
        roulette2.endRotateEvent?.AddListener(SetEndSlot);
     
    }

    private void OnDisable()
    {
        roulette1.endRotateEvent.RemoveListener(SetEndSlot);
        roulette2.endRotateEvent.RemoveListener(SetEndSlot);
    
    }

    private float GetValue()
    {

        return arrow1.collidedObject.Value * 10 + arrow2.collidedObject.Value;
    }

    public void GetResults()
    {
        if (endWorkSlots == 2)
        {
            if (addScore)
            {
                addedScoreEvent?.Invoke();
                scoreCounter.Add(GetValue());
            }
            else
            {
                scoreCounter.TakeAway(GetValue());
                remowedScoreEvent?.Invoke();
            }
            endSpinsEvent?.Invoke();
            addScore = !addScore;
            endWorkSlots = 0;
        }
    }
}
