using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Up(GameObject car, float movement, float speed) {
        for (float i = 0f; i < SecondTask.WH; i += speed * Time.deltaTime) {
            car.transform.position = Vector2.MoveTowards(car.transform.position, new Vector2(car.transform.position.x, car.transform.position.y + SecondTask.WH), speed * Time.deltaTime);
        }
    }

    public static void Down(GameObject car, float movement, float speed) {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            car.transform.position -= new Vector3(0, movement, 0) * speed;
        }
    }

    public static void Left(GameObject car, float movement, float speed) {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            car.transform.position += new Vector3(movement, 0, 0) * speed;
        }
    }

    public static void Right(GameObject car, float movement, float speed) {
        for (float i = 0f; i < SecondTask.WH; i += movement) {
            car.transform.position -= new Vector3(movement, 0, 0) * speed;
        }
    }
}
