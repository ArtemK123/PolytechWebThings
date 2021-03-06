﻿using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateUser;
using Application.Repositories;
using Domain.Entities.Common;
using Domain.Entities.User;
using Domain.Enums;
using Domain.Exceptions;
using Moq;
using NUnit.Framework;

namespace Application.UnitTest.Commands.CreateUser
{
    internal class CreateUserCommandHandlerTest
    {
        private const string UserEmail = "email@somewhere.com";
        private const string Password = "123123";
        private const UserRole Role = UserRole.User;

        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IFactory<NewUserCreationModel, IUser>> newUserFactoryMock;
        private CreateUserCommandHandler createUserCommandHandler;

        [SetUp]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            newUserFactoryMock = new Mock<IFactory<NewUserCreationModel, IUser>>(MockBehavior.Strict);
            createUserCommandHandler = new CreateUserCommandHandler(userRepositoryMock.Object, newUserFactoryMock.Object);
        }

        [Test]
        public void EmailAlreadyTaken_ShouldThrowException()
        {
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(UserEmail)).Returns(Task.FromResult(CreateMockedUser()));
            EmailTakenByOtherUserException exception = Assert.ThrowsAsync<EmailTakenByOtherUserException>(() => createUserCommandHandler.Handle(CreateCommand(), CancellationToken.None));
            Assert.AreEqual($"Email {UserEmail} is already taken by other user", exception?.Message);
        }

        [Test]
        public async Task ValidUser_ShouldAddUser()
        {
            IUser createdUser = CreateMockedUser();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(UserEmail)).Returns(Task.FromResult(default(IUser)));
            newUserFactoryMock
                .Setup(factory => factory.Create(It.Is<NewUserCreationModel>(model => model.Email == UserEmail && model.Password == Password && model.Role == Role)))
                .Returns(createdUser);
            userRepositoryMock.Setup(repo => repo.AddAsync(createdUser)).Returns(Task.CompletedTask);
            await createUserCommandHandler.Handle(CreateCommand(), CancellationToken.None);
        }

        private CreateUserCommand CreateCommand() => new CreateUserCommand(email: UserEmail, password: Password, role: Role);

        private IUser CreateMockedUser()
        {
            var mock = new Mock<IUser>(MockBehavior.Strict);
            mock.SetupGet(user => user.Email).Returns(UserEmail);
            return mock.Object;
        }
    }
}