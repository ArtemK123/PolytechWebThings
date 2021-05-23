using FluentValidation;
using Web.Models.Rules.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Rules
{
    public class DeleteRuleRequestValidator : AbstractValidator<DeleteRuleRequest>
    {
        public DeleteRuleRequestValidator()
        {
            RuleFor(request => request.RuleId).NotNull().SetValidator(new IntIdValidator());
        }
    }
}