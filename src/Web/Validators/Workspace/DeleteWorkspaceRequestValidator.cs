﻿using FluentValidation;
using Web.Models.Workspace.Request;

namespace Web.Validators.Workspace
{
    public class DeleteWorkspaceRequestValidator : AbstractValidator<DeleteWorkspaceRequest>
    {
        public DeleteWorkspaceRequestValidator()
        {
            RuleFor(request => request.WorkspaceId).NotNull().SetValidator(new IdValidator());
        }
    }
}