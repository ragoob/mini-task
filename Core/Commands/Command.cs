using Core.Events;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Commands
{
   public abstract class Command : Message
    {
        /// <summary>
        /// getter and setter FluentValidation to validate model rules
        /// </summary>
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// check if model is valid
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();
    }
}
