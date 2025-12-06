using _12_Fluent_Validation.ViewModels;
using FluentValidation;

namespace _12_Fluent_Validation.Validators
{
    public class KisiAdresViewModelValidator : AbstractValidator<KisiAdresViewModel>
    {

        public KisiAdresViewModelValidator()
        {
            RuleFor(x => x.Kisi.Ad)
                .NotEmpty().WithMessage("Kişi Adı Boş Geçilemez")
                .NotNull().WithMessage("Kişi Adı Null Olamaz")
                .MinimumLength(2).WithMessage("Kişi Adı En Az 3 Karakter Olmalıdır");

            RuleFor(x => x.Kisi.Soyad)
                .NotEmpty().WithMessage("Kişi Soyadı Boş Geçilemez")
                .NotNull().WithMessage("Kişi Soyadı Null Olamaz")
                .MinimumLength(2).WithMessage("Kişi Soyadı En Az 3 Karakter Olmalıdır");

            RuleFor(x => x.Kisi.Yas)
                .NotEmpty().WithMessage("Kişi Yaşı Boş Geçilemez")
                .InclusiveBetween(18, 100).WithMessage("Kişi Yaşı 18 ile 100 arasında olmalıdır.");

            RuleFor(x => x.Adres.Sehir)
                .NotEmpty().WithMessage("Şehir Boş Geçilemez")
                .NotNull().WithMessage("Şehir Null Olamaz")
                .MinimumLength(3).WithMessage("Şehir Adı En Az 3 Karakter Olmalıdır");

            RuleFor(x => x.Adres.AdresTanim)
                .NotEmpty().WithMessage("Adres Tanımı Boş Geçilemez")
                .NotNull().WithMessage("Adres Tanımı Null Olamaz")
                .MinimumLength(10).WithMessage("Adres Tanımı En Az 10 Karakter Olmalıdır");
        }
    }
}
