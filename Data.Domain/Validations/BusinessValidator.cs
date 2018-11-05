using System.Linq;
using FluentValidation;

namespace Project.Domain.Validations {
    public class BusinessValidator : AbstractValidator<Business> {
        public BusinessValidator() {
            var regexItems = RegexItems.Get();
            var numReg = regexItems.First(d => d.Name == RegexItems.Num);

            RuleFor(b => b.No).NotEmpty(); //.WithMessage("商户编号不能为空。");
            RuleFor(b => b.No).Length(2, 10); //.WithMessage("商户编号允许字符数：2-10");
            RuleFor(b => b.No).Matches(numReg.RegexString); //.WithMessage("^[a-z0-9_-]{3,16}$ 格式。");
            RuleFor(b => b.Name).NotEmpty(); //.WithMessage("商户名称不能为空。");
            RuleFor(b => b.Name).Length(2, 50); //.WithMessage("商户名称允许字符数2-50。");
            var cnReg = regexItems.First(d => d.Name == RegexItems.Ncc);
            RuleFor(b => b.Name).Matches(cnReg.RegexString);
        }
    }
}