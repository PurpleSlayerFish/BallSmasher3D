using ECS.Game.Systems.GameCycle;
using ECS.Game.Systems.GameDay;
using ECS.Game.Systems.Linked;
using Leopotam.Ecs;
using Runtime.Game.Utils.MonoBehUtils;
using UnityEngine;
using Zenject;

namespace ECS.Installers
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private ScreenVariables _screenVariables;
        public override void InstallBindings()
        {
            Container.Bind<ScreenVariables>().FromInstance(_screenVariables).AsSingle();
            Container.BindInterfacesAndSelfTo<EcsWorld>().AsSingle().NonLazy();
            BindSystems();
            Container.BindInterfacesTo<EcsMainBootstrap>().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<IsAvailableSetViewSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InstantiateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ElapsedTimeSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PositionRotationTranslateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelStartSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<WalkableViewSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyTargetSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerPickUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerTakeHitSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TriggersDistanceSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<RopeViewSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<DelayCleanUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelEndSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GamePauseSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveGameSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStageSystem>().AsSingle();        //always must been last
            Container.BindInterfacesAndSelfTo<CleanUpSystem>().AsSingle();          //must been latest than last!
        }       
    }
}