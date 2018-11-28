using FluentValidation;
using Sunny.Api.FluentValidation;
using Sunny.Common.Extend.CollectionQuery;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Api.DTO.Request
{
    public class PageInfoValidator : Validator<PageInfo>
    {
        public PageInfoValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex必须大于0");
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize必须大于0");
        }
 
    }
}
