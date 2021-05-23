using MediatR;

namespace Application.Commands.DeleteRule
{
    public class DeleteRuleCommand : IRequest
    {
        public DeleteRuleCommand(int ruleId, string userEmail)
        {
            RuleId = ruleId;
            UserEmail = userEmail;
        }

        public int RuleId { get; }

        public string UserEmail { get; }
    }
}