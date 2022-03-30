using System.Data;
using FluentValidation;

namespace BookStore.BookOperations.GetBook
{
    public class GetBookQueryValidator: AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
        }
    }
}