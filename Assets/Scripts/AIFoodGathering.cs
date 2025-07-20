using UnityEngine;

public class AIFoodGatherer : MonoBehaviour
{
    public float energy = 100f;
    public float maxEnergy = 100f;
    public float energyDepletionRate = 1f; // Depletion per second
    public float wanderSpeed = 20f;
    public Vector2 homePosition; // Starting position
    public float foodSearchRadius = 50f; // Radius in which the AI looks for food
    public float maxFoodGathered = 2; // Max number of food the AI gathers

    public float foodGathered = 0;
    public bool hasFood = false;
    public float dayTimer = 60f; // In seconds
    public float currentTime = 0f; // Track the AI's "day" progress
    public Vector2 currentDestination;
    [SerializeField] private Rigidbody2D _rigidBody2D = null;

    private void Start()
    {
        homePosition = transform.position;
        currentDestination = homePosition;
        _rigidBody2D = GetComponent<Rigidbody2D>();
        InvokeRepeating("Wander", 0f, 1f); // Make AI wander every 1 second
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        energy -= energyDepletionRate * Time.deltaTime; // Energy drains over time

        // If the AI has food, it decides whether to continue looking for more food
        if (hasFood)
        {
            if (foodGathered < maxFoodGathered && (energy > 50 || currentTime < dayTimer / 2))
            {
                // Continue to look for more food
                LookForFood();
            }
            else
            {
                // Return home if food is gathered or energy/time is low
                ReturnHome();
            }
        }
        else
        {
            // Keep searching for food until acquired
            LookForFood();
        }
    }

    private void Wander()
    {
        // Move AI in a random direction away from the home point
        //if (Vector2.Distance(transform.position, homePosition) < 5f) // Ensure AI is not too close to home
        //{
        //    // Random direction within a reasonable range (e.g., random x/y change)
        //    float randomX = Random.Range(-10f, 10f);
        //    float randomY = Random.Range(-10f, 10f);
        //    currentDestination = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
        //}

        float randomX = Random.Range(-50f, 50f);
        float randomY = Random.Range(-50f, 50f);
        currentDestination = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        // Move AI towards the destination point (wander)
        //transform.position = Vector2.MoveTowards(transform.position, currentDestination, wanderSpeed * Time.deltaTime);
        //Vector2 direction = (_playerGameObject.transform.position - transform.position).normalized;
        //Vector2 direction = (currentDestination - transform.position).normalized;
        //_rigidBody2D.linearVelocity = currentDestination * wanderSpeed;
        Vector2 direction = (currentDestination - (Vector2)transform.position).normalized;
        _rigidBody2D.linearVelocity = direction * wanderSpeed;
    }

    private void LookForFood()
    {
        // Simulate looking for food (can be enhanced with collision detection or object finding)
        Collider2D food = Physics2D.OverlapCircle(transform.position, foodSearchRadius, LayerMask.GetMask("Food"));
        if (food != null)
        {
            // If food found, collect it
            //energy = Mathf.Min(maxEnergy, energy + 20f); // Increase energy (adjust as needed)
            hasFood = true;
            foodGathered++;
            Destroy(food.gameObject); // Remove food from the scene
            Debug.Log("Food gathered. Total: " + foodGathered);
        }
    }

    private void ReturnHome()
    {
        // Return to the home position after gathering food
        currentDestination = homePosition;
        transform.position = Vector2.MoveTowards(transform.position, homePosition, wanderSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, homePosition) < 0.5f) // Reached home
        {
            Debug.Log("Returned home.");
            //hasFood = false; // Reset food gathering state
            //foodGathered = 0; // Reset the food count
            //currentTime = 0f; // Reset the day timer
        }
    }
}
