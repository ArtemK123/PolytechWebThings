using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using Microsoft.EntityFrameworkCore;
using PolytechWebThings.Infrastructure.Database.Rules.Steps;

namespace PolytechWebThings.Infrastructure.Database.Rules
{
    internal class RuleRepository : IRuleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RuleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Rule>> GetRulesAsync(int workspaceId)
        {
            IReadOnlyCollection<RuleDatabaseModel> storedRules = await dbContext.Rules.Where(storedRule => storedRule.WorkspaceId == workspaceId).ToArrayAsync();
            return storedRules.Select(Convert).ToList();
        }

        public async Task<Rule?> GetRuleAsync(int workspaceId, string ruleName)
        {
            RuleDatabaseModel? storedRule = await dbContext.Rules.Where(rule => rule.Name == ruleName && rule.WorkspaceId == workspaceId).SingleOrDefaultAsync();
            return storedRule is null ? null : Convert(storedRule);
        }

        public async Task CreateAsync(RuleCreationModel ruleCreationModel)
        {
            RuleDatabaseModel ruleDatabaseModel = new RuleDatabaseModel { Name = ruleCreationModel.Name };

            ruleDatabaseModel.ExecuteRuleSteps = CreateExecuteRuleStepDatabaseModels(ruleCreationModel: ruleCreationModel);
            ruleDatabaseModel.ChangeThingStateSteps = CreateChangeThingStateStepDatabaseModels(ruleCreationModel: ruleCreationModel);
            await dbContext.Rules.AddAsync(ruleDatabaseModel);
            await dbContext.SaveChangesAsync();
        }

        private static List<ExecuteRuleStepDatabaseModel> CreateExecuteRuleStepDatabaseModels(RuleCreationModel ruleCreationModel)
            => ruleCreationModel.Steps
                .Where(step => step.StepType == StepType.ExecuteRule)
                .Select(step =>
                {
                    ExecuteRuleStepCreationModel executeRuleStepCreationModel = (ExecuteRuleStepCreationModel)step;
                    return new ExecuteRuleStepDatabaseModel
                    {
                        RuleName = executeRuleStepCreationModel.RuleName,
                        ExecutionOrderPosition = executeRuleStepCreationModel.ExecutionOrderPosition
                    };
                })
                .ToList();

        private static List<ChangeThingStateStepDatabaseModel> CreateChangeThingStateStepDatabaseModels(RuleCreationModel ruleCreationModel)
            => ruleCreationModel.Steps
                .Where(step => step.StepType == StepType.ChangeThingState)
                .Select(step =>
                {
                    ChangeThingStateStepCreationModel changeThingStateStepCreationModel = (ChangeThingStateStepCreationModel)step;
                    return new ChangeThingStateStepDatabaseModel
                    {
                        ExecutionOrderPosition = changeThingStateStepCreationModel.ExecutionOrderPosition,
                        ThingId = changeThingStateStepCreationModel.ThingId,
                        PropertyName = changeThingStateStepCreationModel.PropertyName,
                        NewPropertyState = changeThingStateStepCreationModel.PropertyName
                    };
                })
                .ToList();

        private Rule Convert(RuleDatabaseModel databaseModel)
        {
            List<Step> steps = new List<Step>();
            steps.AddRange(databaseModel.ExecuteRuleSteps.Select(Convert));
            steps.AddRange(databaseModel.ChangeThingStateSteps.Select(Convert));
            return new Rule(databaseModel.Id, databaseModel.Name, steps, databaseModel.WorkspaceId);
        }

        private Step Convert(ExecuteRuleStepDatabaseModel databaseModel)
        {
            return new ExecuteRuleStep(databaseModel.ExecutionOrderPosition, databaseModel.RuleName);
        }

        private Step Convert(ChangeThingStateStepDatabaseModel databaseModel)
        {
            return new ChangeThingStateStep(
                executionOrderPosition: databaseModel.ExecutionOrderPosition,
                thingId: databaseModel.ThingId,
                propertyName: databaseModel.PropertyName,
                newPropertyState: databaseModel.NewPropertyState);
        }
    }
}