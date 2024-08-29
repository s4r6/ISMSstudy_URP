using UnityEngine;
using Zenject;
using ISMS.Presenter.Detail.Stage;

public class RepositoryInstaller : MonoInstaller
{
    CSVReader _repository = new CSVReader();
    public override void InstallBindings()
    {
        Container
            .Bind<IRepository>()
            .FromInstance(_repository)
            .AsCached();
    }
}