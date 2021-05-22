using FluentValidation;
using Web.Models.Rules.Steps;

namespace Web.Validators.Rules
{
    public class ExecuteRuleStepApiModelValidator : AbstractValidator<StepApiModel>
    {
        public ExecuteRuleStepApiModelValidator()
        {
            RuleFor(step => step.RuleName).NotEmpty();
        }
    }
}