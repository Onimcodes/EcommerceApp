using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.application.Common.Dtos.Constants
{
    public static class ResponseMessages 
    {

        public static string GetSuccessMessage()
        {
            return "Sucessful";
        }

        public static string GetValidationMessage()
        {
            return "A validation error occurred. Please check the input and try again.";
        }
    }
}
