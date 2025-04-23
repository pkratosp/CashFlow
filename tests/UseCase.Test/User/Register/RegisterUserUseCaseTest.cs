using CashFlow.Application.useCases.User.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCase.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        // assert
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();

        // act
        var result = await useCase.Execute(request);


        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Name_Empty_Error()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        request.Name = string.Empty;

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorValidationException>();

        result.Where(ex => ex.GetErrors().Count() == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task Email_Alert_Exist()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorValidationException>();

        result.Where(ex => ex.GetErrors().Count() == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUserUseCase CreateUseCase (string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Build();

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            userReadOnlyRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(
            mapper,
            passwordEncripter,
            userReadOnlyRepository.Build(),
            userWriteOnlyRepository,
            unitOfWork,
            accessTokenGenerator
        );
    }
}
