using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;

namespace Application.Repositories
{
    public interface IRuleRepository
    {
        Task<IReadOnlyCollection<Rule>> GetRulesAsync(int workspaceId);

        Task CreateAsync(RuleCreationModel ruleCreationModel);
    }
}