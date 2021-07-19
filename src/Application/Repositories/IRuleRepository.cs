using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;

namespace Application.Repositories
{
    public interface IRuleRepository
    {
        Task<IReadOnlyCollection<Rule>> GetRulesAsync(int workspaceId);

        Task<Rule?> GetRuleByWorkspaceAndNameAsync(int workspaceId, string ruleName);

        Task<Rule?> GetRuleByIdAsync(int ruleId);

        Task CreateAsync(RuleCreationModel ruleCreationModel);

        Task DeleteAsync(int ruleId);

        Task UpdateRuleNameAsync(int ruleId, string newRuleName);

        Task ClearStepsForRuleAsync(int ruleId);

        Task AddStepsToRuleAsync(int ruleId, IReadOnlyCollection<StepModel> steps);
    }
}