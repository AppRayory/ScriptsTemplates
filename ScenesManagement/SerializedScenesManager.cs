using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

   public class SerializedScenesManager : MonoSingleton<SerializedScenesManager>
    {
        public List<ScenesStruct> ScenesList = new();
        
        [Serializable]
        public struct ScenesStruct {
            public SceneReference SceneReference;
            public EnumsHandler.ScenesDesignation SceneDesignation;
            public ScenesStruct(SceneReference _sceneReference, EnumsHandler.ScenesDesignation _sceneDesignation) {
                SceneReference = _sceneReference;
                SceneDesignation = _sceneDesignation;
            }
        }
        private AsyncOperation _currentAsyncOperation;
        public string MainMenuSceneName;
        public string GameSceneName;

        private void Awake()
        {
            InitializeSingleton();
            SetScenesNames();
            DontDestroyOnLoad(gameObject);
        }

        public void LoadAdditiveScene(string sceneName) {
            StartCoroutine(LoadAdditiveSceneCoroutine(sceneName));
        }
        public void LoadSingleScene(string sceneName) {
            StartCoroutine(LoadSingleSceneCoroutine(sceneName));
        }

        public void UnloadScene(string sceneName) {
            var _gameScene = SceneManager.GetSceneByName(sceneName);
            if (_gameScene.isLoaded) {
                SceneManager.UnloadSceneAsync(sceneName);
            }else {
                return;
            }
        }
        private IEnumerator LoadSingleSceneCoroutine(string sceneName) {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) {
                yield break;
            }
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            _currentAsyncOperation = asyncOperation;
            _currentAsyncOperation.allowSceneActivation = false;
            while (!_currentAsyncOperation.isDone) {
            
                if (_currentAsyncOperation.progress >= 0.9f)
                {
                    _currentAsyncOperation.allowSceneActivation = true;
                }
                yield return null; 
            }
        }
        private IEnumerator LoadAdditiveSceneCoroutine(string sceneName) {
            if (SceneManager.GetSceneByName(sceneName).isLoaded) {
                yield break;
            }
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = false;
            _currentAsyncOperation = asyncOperation;
            while (!asyncOperation.isDone) {

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null; 
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        public string GetSceneNameFromEnum(EnumsHandler.ScenesDesignation scenesDesignation)
        {
            string sceneName = String.Empty;
            var scenes = ScenesList
                .Find(el => el.SceneDesignation == scenesDesignation);
            sceneName = SceneNameFromPath(scenes.SceneReference);

            return sceneName;
        }
        
        private void SetScenesNames() { 
            foreach (var scenes in ScenesList) {
                switch (scenes.SceneDesignation) {
                    case EnumsHandler.ScenesDesignation.MainMenu:
                        MainMenuSceneName = SceneNameFromPath(scenes.SceneReference);
                        break;
                    case EnumsHandler.ScenesDesignation.GamePlay:
                        GameSceneName = SceneNameFromPath(scenes.SceneReference);
                        break;
                    default:
                        continue;
                }
            }
        }
        
        private string SceneNameFromPath(string path) {
            int slash = path.LastIndexOf('/');
            string name = path.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            return name.Substring(0, dot);
        }
    }