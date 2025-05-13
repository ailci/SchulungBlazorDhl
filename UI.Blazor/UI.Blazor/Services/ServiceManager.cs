using Application.Contracts.Services;

namespace UI.Blazor.Services;

public class ServiceManager : IServiceManager
{
    private readonly IQotdService _qotdService;

    public ServiceManager(IQotdService qotdService)
    {
        _qotdService = qotdService;
    }

    public IQotdService QotdService => _qotdService;
}