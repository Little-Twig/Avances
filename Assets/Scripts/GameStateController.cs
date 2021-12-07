using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textdeath;
    // Start is called before the first frame update
    void Start()
    {
        MainCharController.onDeath += OnDeathHandler;
    }

    private void OnDeathHandler()
    {
        textdeath.text = "Game Over";
        Debug.Log("MainCharController activ� la funci�n OnDeathHandler en GameStateController");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

