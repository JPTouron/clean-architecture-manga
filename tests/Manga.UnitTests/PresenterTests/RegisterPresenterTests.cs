namespace Manga.UnitTests.PresenterTests
{
    using System.Net;
    using System;
    using Manga.Application.Boundaries.Register;
    using Manga.Domain.ValueObjects;
    using Manga.WebApi.UseCases.V1.Register;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public sealed class RegisterPresenterTests
    {
        [Fact]
        public void GivenValidData_Handle_WritesOkObjectResult()
        {
            var customer = new Domain.Customers.Customer(
                new SSN("198608179999"),
                new Name("Ivan")
            );

            var account = new Domain.Accounts.Account(
                Guid.NewGuid()
            );

            var registerOutput = new RegisterOutput(
                customer,
                account
            );

            var sut = new RegisterPresenter();
            sut.Standard(registerOutput);

            var actual = Assert.IsType<CreatedAtRouteResult>(sut.ViewModel);
            Assert.Equal((int) HttpStatusCode.Created, actual.StatusCode);

            var actualValue = (RegisterResponse) actual.Value;
            Assert.Equal(customer.Id, actualValue.CustomerId);
        }
    }
}