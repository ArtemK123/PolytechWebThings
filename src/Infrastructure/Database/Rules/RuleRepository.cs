using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;

namespace PolytechWebThings.Infrastructure.Database.Rules
{
    internal class RuleRepository : IRuleRepository
    {
        public Task<IReadOnlyCollection<Rule>> GetRulesAsync(int workspaceId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Rule?> GetRuleAsync(int workspaceId, string ruleName)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(RuleCreationModel ruleCreationModel)
        {
            throw new System.NotImplementedException();
        }
    }
}