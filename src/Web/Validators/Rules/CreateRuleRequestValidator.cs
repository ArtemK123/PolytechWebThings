using FluentValidation;
using Web.Models.Rules.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Rules
{
    public class CreateRuleRequestValidator : AbstractValidator<CreateRuleRequest>
    {
        public CreateRuleRequestValidator()
        {
            RuleFor(request => request.RuleName).NotEmpty();
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IntIdValidator());
            RuleFor(request => request.Steps).NotEmpty();
            RuleForEach(request => request.Steps).NotNull().SetValidator(new StepApiModelValidator());
        }
    }
}