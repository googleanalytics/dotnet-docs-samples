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

Before you start the application, please review the comments starting with
"TODO(developer)" and update the code to use correct values.

This application demonstrates the usage of the Analytics Data API using service
account credentials.

Usage:
  cd analytics-data/QuickStart
  dotnet restore
  dotnet run
 */

// [START google_analytics_data_quickstart]
using Google.Analytics.Data.V1Beta;
using Google.Api.Gax;
using System;

namespace AnalyticsSamples
{
    class QuickStart
    {
        static void SampleRunReport(string propertyId="YOUR-GA4-PROPERTY-ID", string credentialsJsonPath="")
        {
            /**
             * TODO(developer): Uncomment this variable and replace with your
             *  Google Analytics 4 property ID before running the sample.
             */
            // propertyId = "YOUR-GA4-PROPERTY-ID";

            // [START google_analytics_data_initialize]
            /**
             * TODO(developer): Uncomment this variable and replace with a valid path to
             *  the credentials.json file for your service account downloaded from the
             *  Cloud Console.
             *  Otherwise, default service account credentials will be derived from
             *  the GOOGLE_APPLICATION_CREDENTIALS environment variable.
             */
            // credentialsJsonPath = "/path/to/credentials.json";

            BetaAnalyticsDataClient client;
            if(String.IsNullOrEmpty(credentialsJsonPath))
            {
              // Using a default constructor instructs the client to use the credentials
              // specified in GOOGLE_APPLICATION_CREDENTIALS environment variable.
              client = BetaAnalyticsDataClient.Create();
            }
            else
            {
              // Explicitly use service account credentials by specifying
              // the private key file.
              client = new BetaAnalyticsDataClientBuilder
              {
                CredentialsPath = credentialsJsonPath
              }.Build();
            }
            // [END google_analytics_data_initialize]

            // [START google_analytics_data_run_report]
            // Initialize request argument(s)
            RunReportRequest request = new RunReportRequest
            {
                Property = "property/" + propertyId,
                Dimensions = { new Dimension{ Name="city"}, },
                Metrics = { new Metric{ Name="activeUsers"}, },
                DateRanges = { new DateRange{ StartDate="2020-03-31", EndDate="today"}, },
            };

            // Make the request
            PagedEnumerable<RunReportResponse, DimensionHeader> response = client.RunReport(request);
            // [END google_analytics_data_run_report]

            // [START google_analytics_data_print_report]
            Console.WriteLine("Report result:");
            foreach(RunReportResponse page in response.AsRawResponses())
            {
              foreach(Row row in page.Rows)
              {
                  Console.WriteLine("{0}, {1}", row.DimensionValues[0].Value, row.MetricValues[0].Value);
              }
            }
            // [END google_analytics_data_print_report]
        }
        static int Main(string[] args)
        {
            SampleRunReport();
            return 0;
        }
    }
}
// [END google_analytics_data_quickstart]