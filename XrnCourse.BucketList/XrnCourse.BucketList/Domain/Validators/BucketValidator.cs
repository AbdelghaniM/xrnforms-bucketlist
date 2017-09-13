using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrnCourse.BucketList.Domain.Models;

namespace XrnCourse.BucketList.Domain.Validators
{
        public class BucketValidator : AbstractValidator<Bucket>
        {
            public BucketValidator()
            {
                RuleFor(bucket => bucket.Title)
                    .NotEmpty()
                    .WithMessage("Title cannot be empty")
                    .Length(3, 50)
                    .WithMessage("Length must be between 3 and 50");

                RuleFor(bucket => bucket.Description)
                    .NotEqual(b => b.Title)
                    .WithMessage("Must be different than Title")
                    .NotEmpty()
                    .WithMessage("Description cannot be empty");
            }
        }
}
