using FluentValidation;
using MovieStore.DTOLayer.DTOs.ProductDTOs;

namespace MovieStore.BusinessLayer.ValidationRules.ProductValidationRules
{
    public class ProductValidator : AbstractValidator<ResultProjectDTO>
    {
        public ProductValidator()
        {
        }
    }
}
