﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateUser;
using Application.Commands.LoginUser;
using Application.Commands.LogoutUser;
using Application.Queries.GetUserByEmail;
using Domain.Entities.User;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.Controllers
{
    public class UserApiController : ApiControllerBase
    {
        public UserApiController(ISender mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<OperationResult> Create([FromBody]CreateUserRequest createUserRequest, CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new CreateUserCommand(
                    email: createUserRequest.Email ?? throw new NullReferenceException(),
                    password: createUserRequest.Password ?? throw new NullReferenceException(),
                    role: UserRole.User),
                cancellationToken);
            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        public async Task<OperationResult> Login([FromBody]LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new LoginUserCommand(
                    email: loginUserRequest.Email ?? throw new NullReferenceException(),
                    password: loginUserRequest.Password ?? throw new NullReferenceException()),
                cancellationToken);

            IUser user = await Mediator.Send(new GetUserByEmailQuery(email: loginUserRequest.Email));
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                CreateUserPrincipal(user),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });

            return new OperationResult(OperationStatus.Success);
        }

        [HttpPost]
        [Authorize]
        public async Task<OperationResult> Logout(CancellationToken cancellationToken)
        {
            await Mediator.Send(new LogoutUserCommand { Email = UserEmail }, cancellationToken);
            await HttpContext.SignOutAsync();
            return new OperationResult(OperationStatus.Success);
        }

        private ClaimsPrincipal CreateUserPrincipal(IUser user)
        {
            IReadOnlyCollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}