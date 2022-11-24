using System;
using UnityEngine;

namespace UI
{
    public class EndLevelScreenUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelEndScreen;
        [SerializeField] private GameObject levelEndCharacters;

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.EndLevel,OnLevelEnd);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.EndLevel,OnLevelEnd);
        }

        #region OnEvents

        private void OnLevelEnd(EventData eventData)
        {
            if (eventData is EndLevel endLevel)
            {
                levelEndScreen.SetActive(true);
                levelEndCharacters.SetActive(true);
                
                //use the endLevel data to get a set the scores
            }
            else
            {
                Debug.Log("The given Event type of EndLevel is not EventData type EndLevel, please ensure right event is being sent");
            }
        }

        #endregion
    }
}