﻿using Domain.Entities.Rule;
using FluentValidation;
using Web.Models.Rules.Steps;

namespace Web.Validators.Rules
{
    public class StepApiModelValidator : AbstractValidator<StepApiModel>
    {
        public StepApiModelValidator()
        {
            RuleFor(step => step.StepType).IsInEnum();
            RuleFor(step => step.ExecutionOrderPosition).Must(orderPosition => orderPosition.HasValue && orderPosition.Value >= 0);
            RuleFor(step => step).NotNull().SetValidator(new ChangeThingStateStepApiModelValidator()).When(step => step.StepType == StepType.ChangeThingState);
            RuleFor(step => step).NotNull().SetValidator(new ExecuteRuleStepApiModelValidator()).When(step => step.StepType == StepType.ExecuteRule);
        }
    }
}