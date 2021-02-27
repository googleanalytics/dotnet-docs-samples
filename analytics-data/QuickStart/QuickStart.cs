// Copyright(c) 2020 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.

/* Google Analytics Data API sample quickstart application.

Example usage:
    dotnet restore
    dotnet run <GA4 property ID>

This application demonstrates the usage of the Analytics Data API using
service account credentials. For more information on service accounts, see

https://cloud.google.com/iam/docs/understanding-service-accounts

The following document provides instructions on setting service account
credentials for your application:

  https://cloud.google.com/docs/authentication/production

In a nutshell, you need to:
1. Create a service account and download the key JSON file.

https://cloud.google.com/docs/authentication/production#creating_a_service_account

2. Provide service account credentials using one of the following options:
- set the GOOGLE_APPLICATION_CREDENTIALS environment variable, the API
client will use the value of this variable to find the service account key
JSON file.

https://cloud.google.com/docs/authentication/production#setting_the_environment_variable

OR
- manually pass the path to the service account key JSON file to the API client
by specifying the keyFilename parameter in the constructor:
https://cloud.google.com/docs/authentication/production#passing_the_path_to_the_service_account_key_in_code

*/

// [START analyticsdata_quickstart]

using Google.Analytics.Data.V1Alpha;
using System;

namespace AnalyticsSamples
{
    class QuickStart
    {
        static void SampleRunReport(string propertyId)
        {
            // Using a default constructor instructs the client to use the credentials
            // specified in GOOGLE_APPLICATION_CREDENTIALS environment variable.
            AlphaAnalyticsDataClient client = AlphaAnalyticsDataClient.Create();

            // Initialize request argument(s)
            RunReportRequest request = new RunReportRequest
            {
                Entity = new Entity{ PropertyId = propertyId },
                Dimensions = { new Dimension{ Name="city"}, },
                Metrics = { new Metric{ Name="activeUsers"}, },
                DateRanges = { new DateRange{ StartDate="2020-03-31", EndDate="today"}, },
            };

            // Make the request
            RunReportResponse response = client.RunReport(request);

            Console.WriteLine("Report result:");
            foreach( Row row in response.Rows )
            {
                Console.WriteLine("{0}, {1}", row.DimensionValues[0].Value, row.MetricValues[0].Value);
            }
        }

        static int Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Arguments: <GA4 property ID>");
                Console.WriteLine("A GA4 property id parameter is required to make a query to the Google Analytics Data API.");
                return 1;
            }
            string propertyId = args[0];
            SampleRunReport(propertyId);
            return 0;
        }
    }
}
// [END analyticsdata_quickstart]