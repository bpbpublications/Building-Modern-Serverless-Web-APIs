
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Auth.AccessControlPolicy;

namespace TokenAuthorizer
{
    public class Functions
    {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest input, ILambdaContext context)
        {
            var token = input.AuthorizationToken;
            switch (token)
            {
                case "allow":
                    return GeneratePolicy("user", "Allow", input.MethodArn);
                default:
                    return GeneratePolicy("user", "Deny", input.MethodArn);

            }
        }

        private APIGatewayCustomAuthorizerResponse GeneratePolicy(string principalId, string effect, string resource)
        {

            APIGatewayCustomAuthorizerResponse authResponse = new APIGatewayCustomAuthorizerResponse();
            authResponse.PolicyDocument = new APIGatewayCustomAuthorizerPolicy();
            authResponse.PolicyDocument.Version = "2012-10-17";// default version
            authResponse.PolicyDocument.Statement = new System.Collections.Generic.List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>();
            System.Collections.Generic.HashSet<string> Actions = new System.Collections.Generic.HashSet<string>();
            Actions.Add("execute-api:Invoke");
            System.Collections.Generic.HashSet<string> Resources = new System.Collections.Generic.HashSet<string>();
            Resources.Add(resource);

            authResponse.PolicyDocument.Statement.Add(new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement()
            {
                Action = Actions,
                Effect = effect,
                Resource = Resources
            });


            return authResponse;
        }

    }
}
