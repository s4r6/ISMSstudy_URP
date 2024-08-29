using UnityEngine;
using Zenject;
using ISMS.Presenter.Detail.Stage;

public class RepositoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IRepository>()
            .FromInstance(new CSVReader())
            .AsCached();
    }
}