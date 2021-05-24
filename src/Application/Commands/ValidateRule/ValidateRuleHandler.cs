using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.ValidateRule
{
    internal class ValidateRuleHandler : IRequestHandler<ValidateRuleCommand>
    {
        private readonly IRuleRepository ruleRepository;
        private readonly IThingsProvider thingsProvider;

        public ValidateRuleHandler(IRuleRepository ruleRepository, IThingsProvider thingsProvider)
        {
            this.ruleRepository = ruleRepository;
            this.thingsProvider = thingsProvider;
        }

        public async Task<Unit> Handle(ValidateRuleCommand command, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Rule> rules = await ruleRepository.GetRulesAsync(command.Workspace.Id);
            if (rules.Any(rule => rule.Name == command.RuleName))
            {
                throw new NotUniqueEntityException($"Rule with name={command.RuleName} is already created");
            }

            await ValidateStepsAsync(command: command, workspace: command.Workspace, rules: rules);
            return Unit.Value;
        }

        private async Task ValidateStepsAsync(ValidateRuleCommand command, IWorkspace workspace, IReadOnlyCollection<Rule> rules)
        {
            ValidateStepExecutionOrder(command: command);

            IReadOnlyCollection<ExecuteRuleStepModel> executeRuleStepCreationModels =
                command.Steps.Where(step => step.StepType == StepType.ExecuteRule).Select(step => (ExecuteRuleStepModel)step).ToArray();
            IReadOnlyCollection<ChangeThingStateStepModel> changePropertyStateStepCreationModels
                = command.Steps.Where(step => step.StepType == StepType.ChangeThingState).Select(step => (ChangeThingStateStepModel)step).ToArray();

            ValidateExecuteRuleStepModels(executeRuleStepCreationModels, rules);
            await ValidateChangePropertyStateStepModelsAsync(changePropertyStateStepCreationModels, workspace);
        }

        private void ValidateStepExecutionOrder(ValidateRuleCommand command)
        {
            IReadOnlyCollection<StepModel> orderedSteps = command.Steps.OrderBy(step => step.ExecutionOrderPosition).ToArray();
            for (int i = 0; i < orderedSteps.Count; i++)
            {
                StepModel currentStep = orderedSteps.ElementAt(i);
                if (currentStep.ExecutionOrderPosition != i)
                {
                    throw new InvalidStepExecutionOrderException();
                }
            }
        }

        private void ValidateExecuteRuleStepModels(IReadOnlyCollection<ExecuteRuleStepModel> steps, IReadOnlyCollection<Rule> rules)
        {
            foreach (ExecuteRuleStepModel step in steps)
            {
                if (rules.All(rule => rule.Name != step.RuleName))
                {
                    throw new EntityNotFoundException($"Can not find rule with name={step.RuleName}");
                }
            }
        }

        private async Task ValidateChangePropertyStateStepModelsAsync(IReadOnlyCollection<ChangeThingStateStepModel> steps, IWorkspace workspace)
        {
            if (steps.Count == 0)
            {
                return;
            }

            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);

            foreach (ChangeThingStateStepModel step in steps)
            {
                Thing? thing = things.SingleOrDefault(currentThing => currentThing.Id == step.ThingId);

                if (thing is null)
                {
                    throw new EntityNotFoundException($"Can not find thing with id={step.ThingId}");
                }

                Property? property = thing.Properties.SingleOrDefault(currentProperty => currentProperty.Name == step.PropertyName);

                if (property is null)
                {
                    throw new EntityNotFoundException($"Can not find property with name={step.PropertyName}");
                }

                if (!property.IsValidValue(step.NewPropertyState))
                {
                    throw new InvalidPropertyValueException(property.ValueType, step.NewPropertyState);
                }
            }
        }
    }
}