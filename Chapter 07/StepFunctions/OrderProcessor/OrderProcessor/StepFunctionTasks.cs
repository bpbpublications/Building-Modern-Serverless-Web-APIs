using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace OrderProcessor
{
    public class StepFunctionTasks
    {
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public StepFunctionTasks()
        {
        }


        public State ProcessOrder(OrderEvent order, ILambdaContext context)
        {
            Console.WriteLine("OrderProcessing Started");
            Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(order));
            return new State()
            {
                OrderId=order.detail.orderId,
                Message="Process Order completed successfully"
            };
        }

        public State ProcessPayment(State state, ILambdaContext context)
        {
            Console.WriteLine("PaymentProcessing Started");
            Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(state));
            state.Message = "PaymentProcessing Completed";
            Console.Write("PaymentProcessing Completed Successfully");

            return state;
        }

        public State ProcessEmail(State state, ILambdaContext context)
        {
            Console.WriteLine("EmailProcessing Started");
            Console.Write("EmailProcessing Completed Successfully");

            return state;
        }
    }
}
