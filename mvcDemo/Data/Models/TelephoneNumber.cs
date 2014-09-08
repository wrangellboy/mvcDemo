using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [ComplexType]
    public class TelephoneNumber
    {
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        //TODO: Should NumberType be a lookup, or just freeform?
        [Display(Name="Number Type (Home, Work, Cell, etc...")]
        public string NumberType { get; set; }
    }
}