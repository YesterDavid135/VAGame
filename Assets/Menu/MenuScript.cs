using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu {
    public class MenuScript : MonoBehaviour {
        // Called when we click the "Play" button.
        public void OnPlayButton() {
            SceneManager.LoadScene(1);
        }

        // Called when we click the "Quit" button.
        public void OnQuitButton() {
            Application.Quit();
        }
    }
}