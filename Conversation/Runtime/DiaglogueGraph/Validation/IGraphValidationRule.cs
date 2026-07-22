using System.Collections.Generic;

public interface IGraphValidationRule
{
    IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
}