using System.Threading;
using System.Threading.Tasks;
using Application.Users;
using Application.Users.Commands.CreateUser;
using Domain.Entities.Common;
using Domain.Entities.User;
using Domain.Enums;
using Domain.Exceptions;
using Moq;
using NUnit.Framework;

namespace Application.UnitTest.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandlerTest
    {
        private const string UserId = "guid";
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
            userRepositoryMock.Setup(repo => repo.GetByEmail(UserEmail)).Returns(CreateMockedUser());
            EmailTakenByOtherUserException exception = Assert.ThrowsAsync<EmailTakenByOtherUserException>(() => createUserCommandHandler.Handle(CreateCommand(), CancellationToken.None));
            Assert.AreEqual($"Email {UserEmail} is already taken by user with id=${UserId}", exception?.Message);
        }

        [Test]
        public async Task ValidUser_ShouldAddUser()
        {
            IUser createdUser = CreateMockedUser();
            userRepositoryMock.Setup(repo => repo.GetByEmail(UserEmail)).Returns(default(IUser));
            newUserFactoryMock
                .Setup(factory => factory.Create(It.Is<NewUserCreationModel>(model => model.Email == UserEmail && model.Password == Password && model.Role == Role)))
                .Returns(createdUser);
            userRepositoryMock.Setup(repo => repo.Add(createdUser));
            await createUserCommandHandler.Handle(CreateCommand(), CancellationToken.None);
        }

        private CreateUserCommand CreateCommand() => new() { Email = UserEmail, Password = Password, Role = Role };

        private IUser CreateMockedUser()
        {
            var mock = new Mock<IUser>(MockBehavior.Strict);
            mock.SetupGet(user => user.Email).Returns(UserEmail);
            mock.SetupGet(user => user.Id).Returns(UserId);
            return mock.Object;
        }
    }
}