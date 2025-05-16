using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Application.Policy;

public class NoOneIsAllowedToDeleteDefaultAuthorsRequirement : IAuthorizationRequirement
{
}

public class NoOneIsAllowedToDeleteDefaultAuthorsHandler : AuthorizationHandler<NoOneIsAllowedToDeleteDefaultAuthorsRequirement, Guid>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        NoOneIsAllowedToDeleteDefaultAuthorsRequirement requirement, Guid resource)
    {
        if (!IsInitialAuthor(resource))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private bool IsInitialAuthor(Guid authorId)
    {
        var einsteinId = Guid.Parse("78c83367-25de-4780-8a35-0c5636d3ca50");
        var twainId = Guid.Parse("4577dc8b-9648-4b9e-a4c7-3fd2dbf9e6cc");
        var wildeId = Guid.Parse("2ebd31ba-fe9e-4c07-b393-3152fa40ada3");
        var tucholskyId = Guid.Parse("795ca27e-a7f2-41e5-ad97-71174e99e00d");
        var bismarckId = Guid.Parse("c37b5bc0-19db-497f-968b-0e2d05627c8d");
        var bahrId = Guid.Parse("0129f13b-6ee8-4339-94cd-ad22f472eb8e");
        var luxemburgId = Guid.Parse("595f9fb4-f452-40f9-8a11-576d74a343f8");
        var gandhiId = Guid.Parse("a5a914ea-de67-4522-86f5-90ff872c9e84");
        var tolstoiId = Guid.Parse("1b537fa7-3810-435c-8290-be9e5b5143ad");
        var yodaId = Guid.Parse("40792abe-b3bd-44db-acc1-790eee1634c7");

        List<Guid> initialAuthors =
            [einsteinId, twainId, wildeId, tucholskyId, bismarckId, bahrId, luxemburgId, gandhiId, tolstoiId, yodaId];

        return initialAuthors.Contains(authorId);
    }

  
}