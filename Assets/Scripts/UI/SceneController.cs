using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Level
{
    public class SceneController : MonoBehaviour, IService
    {
        [SerializeField] private GameObject _endPanel;

        private EventBus _eventBus;
        private bool _isGamePaused = false;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<OnGameLoose>(EndGame);
            _endPanel.SetActive(false);
            SetTimeScale(false);
        }

        private void EndGame(OnGameLoose signal)
        {
            _endPanel.SetActive(true);
            SetTimeScale(true);
        }

        public void RestartGame()
        {
            SetTimeScale(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        private void SetTimeScale(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
            _isGamePaused = pause;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnGameLoose>(EndGame);
        }
    }
}