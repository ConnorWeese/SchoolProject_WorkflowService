using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Assignment3WindowsFormsApp
{

    public sealed class SunshineCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> City { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        public OutArgument<string> Result { get; set; }

        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string city = context.GetValue(this.City);

            SunshineServiceReference.ServiceClient client = new SunshineServiceReference.ServiceClient();
            SunshineServiceReference.GetSunshine getSun = new SunshineServiceReference.GetSunshine();
            getSun.city = city;

            context.SetValue(this.Result, client.GetSunshine(getSun));

            client.Close();
        }
    }
}
