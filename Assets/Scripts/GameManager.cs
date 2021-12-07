using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private MainCharController playerScript;

    private int playerLevel;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            playerLevel = 1;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        MainCharController.onDeath += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GameOver()
    {
        instance.playerLevel = 1;
        Debug.Log("Game Over");
        Debug.Log("MainCharController activó la función GameOver en GameManager");
    }
    public void AddExp()
    {
        instance.playerLevel += 1;
    }
    public static int getLevel()
    {
        return instance.playerLevel;
    }
    
}
