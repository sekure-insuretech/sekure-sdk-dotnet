using System;

namespace Sekure.Models
{
    public class PolicyHolder
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? ExpeditionDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CityCode { get; set; }

        public PolicyHolder() { }

        public PolicyHolder(string firstName, string secondName, string lastName, string secondLastName, DateTime? birthdate, DateTime? expeditionDate, string gender, string address, string identificationType, string identificationNumber, string maritalStatus, string email, string phoneNumber, string cityCode)
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
            Birthdate = birthdate;
            ExpeditionDate = expeditionDate;
            Gender = gender;
            Address = address;
            IdentificationType = identificationType;
            IdentificationNumber = identificationNumber;
            MaritalStatus = maritalStatus;
            Email = email;
            PhoneNumber = phoneNumber;
            CityCode = cityCode;
        }
    }
}
