using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateRule;
using Application.Converters;
using Application.Queries.GetRuleByWorkspaceAndName;
using Domain.Entities.Rule;
using Domain.Entities.Rule.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
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