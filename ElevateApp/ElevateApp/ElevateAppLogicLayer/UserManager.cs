﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ElevateAppDataAccess;

namespace ElevateAppLogicLayer
{
    public class UserManager
    {
         
        internal static string HashSHA256(string source)
        {
            var result = "";

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));

               
            }

            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }

       
        public static bool AuthenticateUser(string email, string password)
    {
      var result = false;

        try 
	{
        result = (1 == UserAccessor.VerifyEmailAndPassword(email, HashSHA256(password)));
	}
	catch (Exception)
	{
		
		throw;
	}
        return result;
    }
        
        
    }
}
