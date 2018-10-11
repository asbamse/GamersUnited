using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.ApplicationService.Impl.Utilities
{
    class Requirement
    {
        public static void NotNull(Object value, string field)
        {
            if(value == null)
            {
                throw new ArgumentException(field + " cannot be null");
            }
        }

        public static void MinLength(int min, string value, string field)
        {
            NotNull(value, field);
            if (value.Length < min)
            {
                throw new ArgumentException(field + " is shorter than the minimum length of " + min);
            }
        }

        public static void MaxLength(int max, string value, string field)
        {
            NotNull(value, field);
            if (value.Length > max)
            {
                throw new ArgumentException(field + " is larger than the maximum length of " + max);
            }
        }

        public static void MinLength(int min, int value, string field)
        {
            NotNull(value, field);
            if (value < min)
            {
                throw new ArgumentException(field + " is less than the minimum value of " + min);
            }
        }

        public static void MaxLength(int max, int value, string field)
        {
            NotNull(value, field);
            if (value > max)
            {
                throw new ArgumentException(field + " is larger than the maximum value of " + max);
            }
        }

        public static void DanishPhoneNumber(int value, string field)
        {
            NotNull(value, field);
            if (value.ToString().Length != 8)
            {
                throw new ArgumentException(field + " has to be a danish phone number with 8 digit");
            }
        }

        public static void Email(string value, string field)
        {
            NotNull(value, field);
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                if (addr.Address == value)
                {
                    return;
                }
            } catch { }

            throw new ArgumentException(field + " has to be an email address");
        }
    }
}
