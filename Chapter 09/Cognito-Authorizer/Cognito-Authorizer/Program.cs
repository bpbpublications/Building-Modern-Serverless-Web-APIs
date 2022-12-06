using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cognito_Authorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string CLIENT_ID = "";
            string USERPOOL_ID = "";
            string USERNAME = "";
            string PASSWORD = "";
            string SECRET = "";

            AmazonCognitoIdentityProviderClient provider  = new AmazonCognitoIdentityProviderClient(FallbackRegionFactory.GetRegionEndpoint());

            var request = new AdminInitiateAuthRequest()
            {
                AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
                ClientId= CLIENT_ID,
                UserPoolId= USERPOOL_ID,
                
            };

            //aws cognito-idp admin-set-user-password --user-pool-id us-east-1_GKvnPcxax --username awshero --password Pass@word1234 --permanent

            request.AuthParameters.Add("USERNAME", USERNAME);
            request.AuthParameters.Add("PASSWORD", PASSWORD);
            request.AuthParameters.Add("SECRET_HASH", GenerateSecretHash(USERNAME, CLIENT_ID, SECRET));
            var response = provider.AdminInitiateAuthAsync(request).Result;
            Console.WriteLine(response.AuthenticationResult.AccessToken);
            Console.ReadLine();
            
        }

        public static string GenerateSecretHash(string userName, string appClientId, string appSecretKey)
        {
            var str = $"{userName}{appClientId}";

            var userData = Encoding.UTF8.GetBytes(str);
            var secretKey = Encoding.UTF8.GetBytes(appSecretKey);

            return Convert.ToBase64String(HmacSHA256(userData, secretKey));
        }

        public static byte[] HmacSHA256(byte[] data, byte[] key)
        {
            using (var shaAlgorithm = new HMACSHA256(key))
            {
                var result = shaAlgorithm.ComputeHash(data);
                return result;
            }
        }

        
    }
}
