using FluentValidation;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Repositories;

namespace StudentAdminPortalAPI.Validators
{
    public class AddStudentRequestValidator : AbstractValidator<AddStudentRequest>
    {
        public AddStudentRequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(1000000000);
            RuleFor(x => x.GenderID).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                    return true;
                return false;
            }).WithMessage("Please select a valid gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
        }
    }
}
