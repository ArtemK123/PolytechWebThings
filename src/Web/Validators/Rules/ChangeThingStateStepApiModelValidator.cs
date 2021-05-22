using FluentValidation;
using Web.Models.Rules.Steps;

namespace Web.Validators.Rules
{
    public class ChangeThingStateStepApiModelValidator : AbstractValidator<StepApiModel>
    {
        public ChangeThingStateStepApiModelValidator()
        {
            RuleFor(step => step.ThingId).NotNull();
            RuleFor(step => step.PropertyName).NotNull();
        }
    }
}