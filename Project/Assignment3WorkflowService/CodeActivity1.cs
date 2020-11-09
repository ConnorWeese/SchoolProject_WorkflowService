using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Net;

namespace Assignment3WorkflowService
{

    public sealed class CodeActivity1 : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> City { get; set; }
        public OutArgument<Int32> Sun { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string city = context.GetValue(this.City);

            //array of characters used to split the text from the website into an array
            char[] delimitarChars = { '<', '>', '\n' };

            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Download the Web resource and save it into a data buffer.
            byte[] myDataBuffer = myWebClient.DownloadData("https://www.currentresults.com/Weather/US/average-annual-sunshine-by-city.php");
            // Display the downloaded data.
            string download = Encoding.ASCII.GetString(myDataBuffer);

            //split the string into a string array
            string[] words = download.Split(delimitarChars);

            //create a list to add teh data into
            var temp = new List<String>();

            //flag to control when the program can write data to the list
            bool canWrite = false;

            foreach (var word in words)
            {
                //if we are at the end of one of the tables
                if (word == "/tbody")
                {
                    //set canWrite to false
                    canWrite = false;
                }

                //if we can write
                if (canWrite)
                {
                    //if the string we are on is not one of the following
                    if (word != "tr" && word != "/tr" && word != "td" && word != "/td" && word != "")
                    {
                        //add the word to the list
                        temp.Add(word);
                    }
                }

                //if we are at the begining of a table
                if (word == "tbody")
                {
                    //set canWrite to true
                    canWrite = true;
                }
            }

            //write the list into an array so we can iterate through it
            var data = temp.ToArray();

            /*
            *   The data in the array is as follows:
            *       sunshine
            *       city
            *       total hours
            *       clear days
            * 
            *   We only need the sunshine, so we will use this for loop to get that data from the array
            */
            for (int i = 1; i < data.Length; i = i + 4)
            {
                if (data[i] == city)
                {
                    //cast the sunshine data from string to int
                    int x = Int32.Parse(data[i - 1]);

                    context.SetValue(this.Sun, x);

                }
            }

        }

       
    }
}
