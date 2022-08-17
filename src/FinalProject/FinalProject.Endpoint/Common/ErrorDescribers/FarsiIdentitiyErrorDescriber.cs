using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FinalProject.Endpoint.Common.ErrorDescribers
{
    public class FarsiIdentitiyErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format("ایمیل {0} قبلا ثبت شده است.", email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {

            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format("ایمیل {0} قبلا ثبت شده است.", userName)
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = "نام کاربری اشتباه."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = "پسوورد کوتاه است."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = "پسوورد باید حاوی حروف بزرگ باشد."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = "ایمیل اشتباه."
            }; ;
        }
    }
}
