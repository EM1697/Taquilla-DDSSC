using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CineTaquilla.Helpers
{

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }

        public static string GetDescription(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>();
        }
    }

    public enum TicketType
    {
        [Display(Name = "Boleto normal")]
        NormalTicket = 0,

        [Display(Name = "Boleto 3D")]
        Ticket3D = 1,

        [Display(Name = "Boleto VIP")]
        VipTicket = 2,
    }

    public enum SeatStatus
    {
        [Display(Name = "Libre")]
        Free = 0,

        [Display(Name = "Ocupado")]
        Taken = 1,

        [Display(Name = "Bloqueado")]
        Blocked = 2
    }
}
