using dotnet_nopreco.Dtos.Product;
using FluentValidation;

namespace dotnet_nopreco.Middlewares.Product
{
    public class ProductReqValidator : AbstractValidator<ProductReqDto>
    {
        public ProductReqValidator() {
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("name must be at least 3 characters");
            RuleFor(p => p.Description).MinimumLength(8).WithMessage("description must be at least 8 characters");
            RuleFor(p => p.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(p => !string.IsNullOrEmpty(p.ImageUrl))
                .WithMessage("imageUrl must be a valid url");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
            RuleFor(p => p.Category).IsInEnum().WithMessage("Provide a valid category");
        }
    }
}