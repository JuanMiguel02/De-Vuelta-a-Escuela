using UnityEngine;

public class Camara1 : MonoBehaviour
{
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newCamPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = new Vector3(newCamPosition.x, newCamPosition.y, transform.position.z);
    }
}
