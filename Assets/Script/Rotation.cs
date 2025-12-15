using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Rotation : MonoBehaviour
{
    [SerializeField] float xRotate;
    [SerializeField] float yRotate;
    [SerializeField] float zRotate;
    [SerializeField] float speed = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotate, yRotate, zRotate * Time.deltaTime);
    }
}
