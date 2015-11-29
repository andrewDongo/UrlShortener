using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UrlShortening.Web.Models
{
    /// <summary>
    /// Honeypot security View Model to mitigate spam bots from utilizing service.
    /// Spam bots will usually attempt to provide these values 
    /// If timestamp value has been modified returns failed validation result
    /// If pot value has been provided returns failed validation result
    /// </summary>
    public class HoneypotViewModel: IValidatableObject
    {
        public HoneypotViewModel()
        {
            this.Stamp = DateTime.UtcNow.ToString("ff-HH-MM-ff-yy-tss-dd-mm");
        }

        public string Pot { get; set; }
        public string Stamp { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrEmpty(this.Pot))
                return new List<ValidationResult> {new ValidationResult("An error occured")};

            DateTime timestamp;
            if (!DateTime.TryParseExact(this.Stamp, "ff-HH-MM-ff-yy-tss-dd-mm",
                                        null, DateTimeStyles.None, out timestamp))
            {
                return new List<ValidationResult> { new ValidationResult("An error occured") };
            }
            return new[] { ValidationResult.Success };
        }
    }
}