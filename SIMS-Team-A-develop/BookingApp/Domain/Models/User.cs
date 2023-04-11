using InitialProject.Serializer;
using System;

namespace BookingApp.Domain.Models
{

    public enum USER_TYPE
    {
        Customer_Booking,
        Customer_Tours,
        Owner,
        Guide
    }

    public class User : ISerializable
    {

        public int id { get; set; }
        public string username { get; set; } 
        public string password { get; set; } 
        public USER_TYPE userType { get; set; }

        public User() { }

        public User(string username, string password, USER_TYPE usertype)
        {
            this.username = username; 
            this.password = password;
            this.userType = usertype;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                id.ToString(),
                username,
                password,
                userType.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            username = values[1];
            password = values[2];
            userType = parseUserType(values[3]);
        }

        private USER_TYPE parseUserType(string inputStr)
        {

            switch (inputStr)
            {

                case "Customer_Booking":
                    return USER_TYPE.Customer_Booking;

                case "Customer_Tours":
                    return USER_TYPE.Customer_Tours;

                case "Owner":
                    return USER_TYPE.Owner;

                case "Guide":
                    return USER_TYPE.Guide;

                default:
                    return USER_TYPE.Customer_Booking;

            }
        }
    }
}
