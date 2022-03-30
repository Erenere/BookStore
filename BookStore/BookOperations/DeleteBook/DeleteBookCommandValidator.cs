﻿using System.Data;
using FluentValidation;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
            
        }
    }
}