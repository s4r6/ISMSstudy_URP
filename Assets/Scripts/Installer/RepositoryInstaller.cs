using UnityEngine;
using Zenject;
using ISMS.Presenter.Detail.Stage;

public class RepositoryInstaller : MonoInstaller
{
    TextReader _repository = new TextReader();
    public override void InstallBindings()
    {
        Container
            .Bind<IRepository>()
            .FromInstance(_repository)
            .AsCached();
    }
}