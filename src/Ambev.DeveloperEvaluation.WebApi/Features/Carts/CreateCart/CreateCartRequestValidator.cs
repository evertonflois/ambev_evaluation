using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:    
        /// - UserId: Required, length must be 36
        /// - Date: Required
        /// - Products: at least one ocurrence        
        /// </remarks>
        public CreateCartRequestValidator()
        {
            RuleFor(order => order.UserId).NotEmpty().Length(36, 36).WithMessage("User required");
            RuleFor(order => order.Date).NotEmpty().WithMessage("Date required");
            RuleFor(order => order.Products).NotNull().Must(p => p.Count > 0).WithMessage("At least one item");
            RuleFor(order => order.Customer).NotNull().Must(c => !string.IsNullOrEmpty(c.Document) && !string.IsNullOrEmpty(c.Name))
                .WithMessage("Customer required");
        }
    }
}
