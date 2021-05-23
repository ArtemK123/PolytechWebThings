using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            IReadOnlyCollection<RuleDatabaseModel> storedRules = await GetRulesAsync(rule => rule.WorkspaceId == workspaceId);
            return storedRules.Select(Convert).ToList();
        }

        public async Task<Rule?> GetRuleByWorkspaceAndNameAsync(int workspaceId, string ruleName)
        {
            IReadOnlyCollection<RuleDatabaseModel> storedRules = await GetRulesAsync(rule => rule.WorkspaceId == workspaceId && rule.Name == ruleName);
            RuleDatabaseModel? storedRule = storedRules.SingleOrDefault();
            return ConvertNullable(storedRule);
        }

        public async Task<Rule?> GetRuleByIdAsync(int ruleId)
        {
            IReadOnlyCollection<RuleDatabaseModel> storedRules = await GetRulesAsync(rule => rule.Id == ruleId);
            RuleDatabaseModel? storedRule = storedRules.SingleOrDefault();
            return ConvertNullable(storedRule);
        }

        public async Task CreateAsync(RuleCreationModel ruleCreationModel)
        {
            RuleDatabaseModel ruleDatabaseModel = new RuleDatabaseModel { WorkspaceId = ruleCreationModel.WorkspaceId, Name = ruleCreationModel.Name };

            ruleDatabaseModel.ExecuteRuleSteps = CreateExecuteRuleStepDatabaseModels(ruleCreationModel: ruleCreationModel);
            ruleDatabaseModel.ChangeThingStateSteps = CreateChangeThingStateStepDatabaseModels(ruleCreationModel: ruleCreationModel);
            await dbContext.Rules.AddAsync(ruleDatabaseModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int ruleId)
        {
            RuleDatabaseModel? modelWithoutDependencies = await dbContext.Rules.SingleOrDefaultAsync(rule => rule.Id == ruleId);
            if (modelWithoutDependencies is null)
            {
                return;
            }

            dbContext.Rules.Remove(modelWithoutDependencies);
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
                        NewPropertyState = changeThingStateStepCreationModel.NewPropertyState
                    };
                })
                .ToList();

        private async Task<IReadOnlyCollection<RuleDatabaseModel>> GetRulesAsync(Expression<Func<RuleDatabaseModel, bool>> rulesFilter)
            => await dbContext
                .Rules
                .Where(rulesFilter)
                .Include(rule => rule.ExecuteRuleSteps)
                .Include(rule => rule.ChangeThingStateSteps)
                .ToArrayAsync();

        private Rule? ConvertNullable(RuleDatabaseModel? databaseModel) => databaseModel is null ? null : Convert(databaseModel);

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