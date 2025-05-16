using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Application.Policy;

public class ProfessionHandler : AuthorizationHandler<ProfessionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfessionRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == nameof(Profession)))
        {
            var professionStatus = (Profession) Enum.Parse(typeof(Profession),
                context.User.FindFirst(c => c.Type == nameof(Profession)).Value);

            if (professionStatus >= requirement.Profession)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}