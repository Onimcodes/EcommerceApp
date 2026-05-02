using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.application.Common.Constants
{
    public static  class ResponseMessages
    {
        public static string GetSuccessMessage()
        {
            return "Successful";
        }
        public static string GetUserForbidenMessage(string walletType)
        {
            return $"You cannot create a {walletType} wallet type with this user type";
        }
        public static string GetDuplicateMessage(string entityName, string Id)
        {
            return $"{entityName} with id {Id} already exist";
        }
        public static string GetNotFoundMessage(string entityName, string Id)
        {
            return $"{entityName} with id {Id} does not exist";
        }
    }
}
