﻿using Runtime.Game.Ui.Windows.ConsentPopUp;
using Runtime.Game.Ui.Windows.FocusSpace;
using Runtime.Game.Ui.Windows.GameOver;
using Runtime.Game.Ui.Windows.InGameButtons;
using Runtime.Game.Ui.Windows.InGameMenu;
using Runtime.Game.Ui.Windows.LevelComplete;
using Runtime.Game.Ui.Windows.StartToPlay;
using Runtime.Game.Ui.Windows.TouchPad;
using Runtime.Installers;
using Runtime.UI.QuitConcentPopUp;
using SimpleUi;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/GameUiPrefabInstaller", fileName = "GameUiPrefabInstaller")]
    public class GameUiPrefabInstaller : ScriptableObjectInstaller
    {
        [FormerlySerializedAs("Canvas"), SerializeField]
        private Canvas canvas;

        [SerializeField] private InGameMenuView inGameMenu;
        [SerializeField] private FocusView focusView;
        [SerializeField] private ConsentPopUpTarget consentPopUpTarget;
        [SerializeField] private TouchpadView touchpadView;
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private LevelCompleteView levelCompleteView;
        [SerializeField] private InGameMenuView inGameMenuView;
        [SerializeField] private InGameButtonsView inGameButtonsView;
        [SerializeField] private StartToPlayView startToPlayView;

        public override void InstallBindings()
        {
            var canvasObj = Instantiate(canvas);
            var canvasTransform = canvasObj.transform;
            var camera = canvasTransform.GetComponentInChildren<Camera>();
            camera.clearFlags = CameraClearFlags.Depth;
            camera.orthographic = false;
            camera.transform.SetParent(null);

            Container.Bind<Canvas>().FromInstance(canvasObj).AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(camera).AsSingle().WithConcreteId(ECameraType.GameCamera).NonLazy();

            Container.BindUiView<InGameMenuController, InGameMenuView>(inGameMenu, canvasTransform);
            Container.BindUiView<FocusViewController, FocusView>(focusView, null);
            Container.BindUiView<ConsentPopUpViewController, ConsentPopUpTarget>(consentPopUpTarget, canvasTransform);
            Container.BindUiView<TouchpadViewController, TouchpadView>(touchpadView, canvasTransform);
            Container.BindUiView<GameOverController, GameOverView>(gameOverView, canvasTransform);
            Container.BindUiView<LevelCompleteController, LevelCompleteView>(levelCompleteView, canvasTransform);
            Container.BindUiView<InGameButtonsController, InGameButtonsView>(inGameButtonsView, canvasTransform);
            Container.BindUiView<StartToPlayController, StartToPlayView>(startToPlayView, canvasTransform);
        }
    }
}