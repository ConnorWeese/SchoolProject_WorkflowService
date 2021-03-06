﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Assignment3WindowsFormsApp
{

    public sealed class EncryptCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> ToEncrypt { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        public OutArgument<string> Result { get; set; }
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string toEncrypt = context.GetValue(this.ToEncrypt);

            if (toEncrypt != "")
            {
                CryptoService.ServiceClient client = new CryptoService.ServiceClient();
                context.SetValue(this.Result, client.Encrypt(toEncrypt));
                client.Close();
            }
            else
            {
                context.SetValue(this.Result, "Please use the Sunshine service first");
            }
        }
    }
}
