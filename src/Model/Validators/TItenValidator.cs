using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Locadora.Domain;
using FluentValidation;
using Simple;

namespace Locadora.Validators
{
    public class TItenValidator : AbstractValidator<TIten>
    {
        public TItenValidator()
        {
        }
    }
}