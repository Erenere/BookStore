﻿using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentValidation;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(-1);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}