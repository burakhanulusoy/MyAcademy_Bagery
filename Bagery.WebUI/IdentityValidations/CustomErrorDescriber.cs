using Microsoft.AspNetCore.Identity;

namespace Bagery.WebUI.IdentityValidations
{
    public class CustomErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Şifreniz en az bir rakam (0-9) içermelidir."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Şifreniz en az bir küçük harf (a-z) içermelidir."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Şifreniz en az bir büyük harf (A-Z) içermelidir."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Şifreniz en az bir özel karakter (ör. @, #, $, vb.) içermelidir."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"Şifreniz en az {length} karakter uzunluğunda olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = $"Şifreniz birbirinden farklı en az {uniqueChars} karakter içermelidir."
            };
        }


        //şifre değitri ksıısı için lazım
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = "Girdiğiniz şifreler birbiriyle eşleşmiyor. Lütfen kontrol edip tekrar deneyin."
            };
        }




        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"'{userName}' kullanıcı adı başka bir üyemiz tarafından kullanılmaktadır. Lütfen farklı bir kullanıcı adı belirleyin."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"'{email}' e-posta adresi sistemimizde zaten kayıtlıdır. Şifrenizi unuttuysanız 'Şifremi Unuttum' adımını kullanabilirsiniz."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = "Lütfen geçerli bir e-posta adresi formatı giriniz."
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = "Kullanıcı adınız geçersiz karakterler içeriyor. Lütfen yalnızca harf, rakam ve izin verilen sembolleri kullanın."
            };
        }





    }
}
