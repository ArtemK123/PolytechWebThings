using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.MozillaGateway.Providers;
using Application.Queries.GetWorkspaceById;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using Domain.Entities.WebThingsGateway.Properties;
using Domain.Entities.WebThingsGateway.Things;
using Domain.Entities.Workspace;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.CreateRule
{
    internal class CreateRuleRequestHandler : IRequestHandler<CreateRuleRequest>
    {
        private readonly ISender mediator;
        private readonly IRuleRepository ruleRepository;
        private readonly IThingsProvider thingsProvider;

        public CreateRuleRequestHandler(ISender mediator, IRuleRepository ruleRepository, IThingsProvider thingsProvider)
        {
            this.mediator = mediator;
            this.ruleRepository = ruleRepository;
            this.thingsProvider = thingsProvider;
        }

        public async Task<Unit> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            IWorkspace workspace = await mediator.Send(new GetWorkspaceByIdQuery(request.WorkspaceId, request.UserEmail), cancellationToken);
            await ValidateRuleCreationModelAsync(request, workspace);
            await ruleRepository.CreateAsync(request.RuleCreationModel);
            return Unit.Value;
        }

        private async Task ValidateRuleCreationModelAsync(CreateRuleRequest request, IWorkspace workspace)
        {
            IReadOnlyCollection<Rule> rules = await ruleRepository.GetRulesAsync(workspace.Id);
            if (rules.Any(rule => rule.Name == request.RuleCreationModel.Name))
            {
                throw new NotUniqueEntityException($"Rule with name={request.RuleCreationModel.Name} is already created");
            }

            await ValidateStepsAsync(request: request, workspace: workspace, rules: rules);
        }

        private async Task ValidateStepsAsync(CreateRuleRequest request, IWorkspace workspace, IReadOnlyCollection<Rule> rules)
        {
            IReadOnlyCollection<ExecuteRuleStepCreationModel> executeRuleStepCreationModels =
                request.RuleCreationModel.Steps.Where(step => step.StepType == StepType.ExecuteRule).Select(step => (ExecuteRuleStepCreationModel)step).ToArray();
            IReadOnlyCollection<ChangePropertyStateStepCreationModel> changePropertyStateStepCreationModels
                = request.RuleCreationModel.Steps.Where(step => step.StepType == StepType.ChangeThingState).Select(step => (ChangePropertyStateStepCreationModel)step).ToArray();

            ValidateExecuteRuleStepModels(executeRuleStepCreationModels, rules);
            await ValidateChangePropertyStateStepModelsAsync(changePropertyStateStepCreationModels, workspace);
        }

        private void ValidateExecuteRuleStepModels(IReadOnlyCollection<ExecuteRuleStepCreationModel> steps, IReadOnlyCollection<Rule> rules)
        {
            foreach (ExecuteRuleStepCreationModel step in steps)
            {
                if (rules.All(rule => rule.Name != step.RuleName))
                {
                    throw new EntityNotFoundException($"Can not find rule with name=${step.RuleName}");
                }
            }
        }

        private async Task ValidateChangePropertyStateStepModelsAsync(IReadOnlyCollection<ChangePropertyStateStepCreationModel> steps, IWorkspace workspace)
        {
            if (steps.Count == 0)
            {
                return;
            }

            IReadOnlyCollection<Thing> things = await thingsProvider.GetAsync(workspace);

            foreach (ChangePropertyStateStepCreationModel step in steps)
            {
                Thing? thing = things.SingleOrDefault(currentThing => currentThing.Id == step.ThingId);

                if (thing is null)
                {
                    throw new EntityNotFoundException($"Can not find thing with id=${step.ThingId}");
                }

                Property? property = thing.Properties.SingleOrDefault(currentProperty => currentProperty.Name == step.PropertyName);

                if (property is null)
                {
                    throw new EntityNotFoundException($"Can not find property with name=${step.PropertyName}");
                }

                if (!property.IsValidValue(step.NewPropertyState))
                {
                    throw new InvalidPropertyValueException(property.ValueType, step.NewPropertyState);
                }
            }
        }
    }
}