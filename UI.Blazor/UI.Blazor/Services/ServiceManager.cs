using Application.Contracts.Services;

namespace UI.Blazor.Services;

public class ServiceManager : IServiceManager
{
    private readonly IQotdService _qotdService;
    private readonly IAuthorService _authorService;

    public ServiceManager(IQotdService qotdService, IAuthorService authorService)
    {
        _qotdService = qotdService;
        _authorService = authorService;
    }

    public IQotdService QotdService => _qotdService;
    public IAuthorService AuthorService => _authorService;
}