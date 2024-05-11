using FluentValidation;
using FourMinator.Persistence.Domain;


namespace FourMinator.RobotService.Services
{
    internal class RobotValidator : AbstractValidator<Robot>
    {
        public RobotValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(r => r.Name).MaximumLength(32).WithMessage("Name cannot be longer than 32 characters");
        }
    }
}
