using Budget.Converter;
using System.ComponentModel;

namespace Budget.Enum
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum CategoryEnum
    {
        [Description("Groceries")]
        Groceries = 1,
        [Description("Dining Out")]
        DiningOut,
        [Description("Mortgage")]
        Mortgage,
        [Description("Adventure")]
        Adventure,
        [Description("Gas")]
        Gas,
        [Description("Gifts")]
        Gifts,
        [Description("Household")]
        Household,
        [Description("Shopping")]
        Shopping,
        [Description("Entertainment")]
        Entertainment,
        [Description("Coffee")]
        Coffee,
        [Description("Cleaners")]
        Cleaners,
        [Description("Travel")]
        Travel,
        [Description("Parking")]
        Parking,
        [Description("Haircut")]
        Haircut
    }
}
