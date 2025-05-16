using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Application.Policy;

public  class ProfessionRequirement(Profession profession) : IAuthorizationRequirement
{
    public Profession Profession { get; set; } = profession;
}