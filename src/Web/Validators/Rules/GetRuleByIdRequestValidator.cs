using FluentValidation;
using Web.Models.Rules.Request;
using Web.Validators.Workspace;

namespace Web.Validators.Rules
{
    public class GetRuleByIdRequestValidator : AbstractValidator<GetRuleByIdRequest>
    {
        public GetRuleByIdRequestValidator()
        {
            RuleFor(request => request.RuleId).NotNull().SetValidator(new IntIdValidator());
        }
    }
}