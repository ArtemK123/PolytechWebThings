﻿using FluentValidation;
using Web.Models.Workspace.Request;

namespace Web.Validators.Workspace
{
    public class GetWorkspaceByIdRequestValidator : AbstractValidator<GetWorkspaceByIdRequest>
    {
        public GetWorkspaceByIdRequestValidator()
        {
            RuleFor(request => request.Id).NotNull().SetValidator(new IntIdValidator());
        }
    }
}