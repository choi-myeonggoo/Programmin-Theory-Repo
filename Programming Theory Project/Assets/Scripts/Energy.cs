using UnityEngine;

[System.Serializable]
public class Energy
{
    [SerializeField] protected float maxValue;
    public float MaxValue
    {
        get { return maxValue; }
        protected set { maxValue = Mathf.Clamp(value, 1, Mathf.Infinity); }
    }
    [SerializeField] protected float regeneration;
    public float Regeneration
    {
        get { return regeneration; }
        protected set { regeneration = Mathf.Clamp(value, 0, Mathf.Infinity); ; }
    }
    [SerializeField] protected float currentValue;
    public float CurrentValue
    {
        get { return currentValue; }
        private set { currentValue = Mathf.Clamp(value, 0, MaxValue); ; }
    }
    public void RecoverAll()
    {
        CurrentValue = MaxValue;
    }
    public void IncreaseCurrentValue(float value)
    {
        CurrentValue += value;
    }
    public void DecreaseCurrentValue(float value)
    {
        CurrentValue -= value;
    }
    public void IncreaseMaxValue(float value)
    {
        MaxValue += value;
    }
    public void DecreaseMaxValue(float value)
    {
        MaxValue -= value;

    }
    public void IncreaseRegeneration(float value)
    {
        Regeneration += value;

    }
    public void DecreaseRegeneration(float value)
    {
        Regeneration -= value;
    }
}