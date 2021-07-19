using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetRuleById;
using Application.Queries.GetRuleByWorkspaceAndName;
using Application.Repositories;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.UpdateRule
{
    internal class UpdateRuleHandler : IRequestHandler<UpdateRuleCommand>
    {
        private readonly ISender mediator;
        private readonly IRuleRepository ruleRepository;

        public UpdateRuleHandler(ISender mediator, IRuleRepository ruleRepository)
        {
            this.mediator = mediator;
            this.ruleRepository = ruleRepository;
        }

        public async Task<Unit> Handle(UpdateRuleCommand request, CancellationToken cancellationToken)
        {
            Rule rule = await mediator.Send(new GetRuleByIdQuery(request.RuleId, request.UserEmail), cancellationToken);
            if (rule.Name != request.NewRuleName)
            {
                await ValidateNewRuleNameAsync(request, rule.WorkspaceId);
            }

            ValidateUpdatedSteps(request: request, workspaceId: rule.WorkspaceId);

            await UpdateRuleAsync(request, originalRule: rule);

            return Unit.Value;
        }

        private async Task ValidateNewRuleNameAsync(UpdateRuleCommand request, int workspaceId)
        {
            try
            {
                Rule storedRule = await mediator.Send(new GetRuleByWorkspaceAndNameQuery(workspaceId, userEmail: request.UserEmail, ruleName: request.NewRuleName));
                if (storedRule is not null)
                {
                    throw new NotUniqueEntityException($"Rule with name={request.NewRuleName} already exists in workspace with id={workspaceId}");
                }
            }
            catch (EntityNotFoundException)
            {
            }
        }

        private void ValidateUpdatedSteps(UpdateRuleCommand request, int workspaceId)
        {
            if (request.UpdatedSteps.Count == 0)
            {
                throw new EmptyCollectionException("Steps collection should not be empty. Add at least one step");
            }

            ValidateExecutionOrder(request);
            ValidateCircularReferences(request: request, workspaceId);
        }

        private void ValidateExecutionOrder(UpdateRuleCommand request)
        {
            StepModel[] orderedSteps = request.UpdatedSteps.OrderBy(step => step.ExecutionOrderPosition).ToArray();

            for (int i = 0; i < orderedSteps.Length; i++)
            {
                if (orderedSteps[i].ExecutionOrderPosition != i)
                {
                    throw new BrokenStepExecutionOrderException("Step execution order is broken.");
                }
            }
        }

        private void ValidateCircularReferences(UpdateRuleCommand request, int workspaceId)
        {
            IEnumerable<Rule> referencedRules =
                request.UpdatedSteps
                    .Where(step => step.StepType == StepType.ExecuteRule)
                    .Select(async step => await GetReferencedRuleAsync(workspaceId, (ExecuteRuleStepModel)step))
                    .Select(task => task.Result)
                    .Where(rule => rule is not null)
                    .Select(rule => rule!);

            IEnumerable<ExecuteRuleStep> executeRuleStepsInReferencedRules =
                referencedRules
                    .SelectMany(rule => rule.Steps)
                    .Where(step => step.StepType == StepType.ExecuteRule)
                    .Select(step => (ExecuteRuleStep)step);

            if (executeRuleStepsInReferencedRules.Any(step => step.RuleName == request.NewRuleName))
            {
                throw new CircularReferenceException("Circular reference between steps was detected");
            }
        }

        private async Task<Rule?> GetReferencedRuleAsync(int workspaceId, ExecuteRuleStepModel step)
            => await ruleRepository.GetRuleByWorkspaceAndNameAsync(workspaceId, step.RuleName);

        private async Task UpdateRuleAsync(UpdateRuleCommand request, Rule originalRule)
        {
            if (request.NewRuleName != originalRule.Name)
            {
                await ruleRepository.UpdateRuleNameAsync(request.RuleId, request.NewRuleName);
            }

            await ruleRepository.ClearStepsForRuleAsync(request.RuleId);
            await ruleRepository.AddStepsToRuleAsync(request.RuleId, request.UpdatedSteps);
        }
    }
}