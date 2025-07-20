using UnityEngine;

public class Unit : MonoBehaviour
{
    public float energy = 0;
    public float energyConsumptionRate = 0;
    public float speed = 0;
    //public float sense;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        energy = 50;
        speed = 1;
        energyConsumptionRate = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wander()
    {
        if (energy < 0)
        {

        }
    }
}
