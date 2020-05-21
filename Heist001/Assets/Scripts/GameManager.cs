using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SecurityLevel {
    Level0 = 0,
    Level1 = 1,
    Level2 = 2
}

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager gameManager { get; private set; } = null;

    private void Awake() {
        if (gameManager != null && gameManager != this) {
            Destroy(this.gameObject);
        }

        gameManager = this;
    }
    #endregion
    
    public SecurityLevel securityLevelAcess = SecurityLevel.Level0;

    public void UgradeSecurityLevel(SecurityLevel level) {
        securityLevelAcess = level;
    }

    public void Fail() {
        Debug.Log("Game over = restart (DEBUG)");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
