using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateRule;
using Application.Commands.DeleteRule;
using Application.Converters;
using Application.Queries.GetRuleByWorkspaceAndName;
using Application.Queries.GetRulesFromWorkspace;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;

namespace Web.Controllers
{
    public class RulesApiController : ApiControllerBase
    {
        public RulesApiController(ISender mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<CreateRuleResponse>> Create([FromBody] CreateRuleRequest request, CancellationToken cancellationToken)
        {
            CreateRuleRequest ruleCreationModel = NullableConverter.GetOrThrow(request);
            int workspaceId = NullableConverter.GetOrThrow(ruleCreationModel.WorkspaceId);

            await Mediator.Send(
                new CreateRuleCommand(
                    workspaceId: workspaceId,
                    UserEmail,
                    Convert(ruleCreationModel)),
                cancellationToken);

            Rule createdRule = await Mediator.Send(
                new GetRuleByWorkspaceAndNameQuery(workspaceId, UserEmail, NullableConverter.GetOrThrow(ruleCreationModel.RuleName)),
                cancellationToken);

            return new OperationResult<CreateRuleResponse>(OperationStatus.Success, new CreateRuleResponse { CreatedRuleId = createdRule.Id });
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult<GetAllFromWorkspaceResponse>> GetAllFromWorkspace([FromBody] GetAllFromWorkspaceRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Rule> rules = await Mediator.Send(new GetRulesFromWorkspaceQuery(NullableConverter.GetOrThrow(request.WorkspaceId), UserEmail), cancellationToken);
            IReadOnlyCollection<RuleApiModel> convertedRules = rules.Select(Convert).ToArray();
            return new OperationResult<GetAllFromWorkspaceResponse>(OperationStatus.Success, new GetAllFromWorkspaceResponse { Rules = convertedRules });
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Delete([FromBody] DeleteRuleRequest request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteRuleCommand(NullableConverter.GetOrThrow(request.RuleId), UserEmail), cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        private RuleApiModel Convert(Rule rule)
        {
            return new RuleApiModel
            {
                Id = rule.Id,
                Name = rule.Name,
                WorkspaceId = rule.WorkspaceId,
                Steps = rule.Steps.Select(Convert).ToArray()
            };
        }

        private StepApiModel Convert(Step step)
        {
            StepApiModel stepApiModel = new StepApiModel
            {
                ExecutionOrderPosition = step.ExecutionOrderPosition,
                StepType = step.StepType
            };

            return step switch
            {
                ExecuteRuleStep executeRuleStep => stepApiModel with
                {
                    RuleName = executeRuleStep.RuleName
                },
                ChangeThingStateStep changeThingStateStep => stepApiModel with
                {
                    ThingId = changeThingStateStep.ThingId,
                    PropertyName = changeThingStateStep.PropertyName,
                    NewPropertyState = changeThingStateStep.NewPropertyState
                },
                _ => throw new NotSupportedException()
            };
        }

        private RuleCreationModel Convert(CreateRuleRequest apiModel)
        {
            return new RuleCreationModel(
                NullableConverter.GetOrThrow(apiModel.WorkspaceId),
                NullableConverter.GetOrThrow(apiModel.RuleName),
                NullableConverter.GetOrThrow(apiModel.Steps).Select(Convert).ToArray());
        }

        private StepCreationModel Convert(StepApiModel apiModel)
        {
            if (apiModel.StepType == StepType.ExecuteRule)
            {
                return new ExecuteRuleStepCreationModel(
                    executionOrderPosition: NullableConverter.GetOrThrow(apiModel.ExecutionOrderPosition),
                    stepType: StepType.ExecuteRule,
                    ruleName: NullableConverter.GetOrThrow(apiModel.RuleName));
            }

            if (apiModel.StepType == StepType.ChangeThingState)
            {
                return new ChangeThingStateStepCreationModel(
                    executionOrderPosition: NullableConverter.GetOrThrow(apiModel.ExecutionOrderPosition),
                    stepType: StepType.ChangeThingState,
                    thingId: NullableConverter.GetOrThrow(apiModel.ThingId),
                    propertyName: NullableConverter.GetOrThrow(apiModel.PropertyName),
                    newPropertyState: apiModel.NewPropertyState);
            }

            throw new NotSupportedException($"Not supported step type ${apiModel.StepType}");
        }
    }
}