﻿using Initializers;
using Runtime.Game.Ui.Windows.SplashScreen.Impls;
using Zenject;

namespace Installers
{
    public class SplashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SplashScreenWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<SplashInitializer>().AsSingle();
        }
    }
}