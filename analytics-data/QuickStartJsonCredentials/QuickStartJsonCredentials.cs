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
  cd analytics-data/QuickStartJsonCredentials
  dotnet restore
  dotnet run
 */

// [START analyticsdata_json_credentials_quickstart]
using Google.Analytics.Data.V1Beta;
using System;

namespace AnalyticsSamples
{
    class QuickStartJsonCredentials
    {
        static void SampleRunReport(string propertyId="YOUR-GA4-PROPERTY-ID", string credentialsJsonPath="")
        {
            /**
             * TODO(developer): Uncomment this variable and replace with your
             *  Google Analytics 4 property ID before running the sample.
             */
            // propertyId = "YOUR-GA4-PROPERTY-ID";

            // [START analyticsdata_json_credentials_initialize]
            /**
             * TODO(developer): Uncomment this variable and replace with a valid path to
             *  the credentials.json file for your service account downloaded from the
             *  Cloud Console.
             *  Otherwise, default service account credentials will be derived from
             *  the GOOGLE_APPLICATION_CREDENTIALS environment variable.
             */
            // credentialsJsonPath = "/path/to/credentials.json";

            // Explicitly use service account credentials by specifying
            // the private key file.
            BetaAnalyticsDataClient client = new BetaAnalyticsDataClientBuilder
            {
              CredentialsPath = credentialsJsonPath
            }.Build();
            // [END analyticsdata_json_credentials_initialize]

            // [START analyticsdata_json_credentials_run_report]
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
            // [END analyticsdata_json_credentials_run_report]

            // [START analyticsdata_json_credentials_run_report_response]
            Console.WriteLine("Report result:");
            foreach(RunReportResponse page in response.AsRawResponses())
            {
              foreach(Row row in page.Rows)
              {
                  Console.WriteLine("{0}, {1}", row.DimensionValues[0].Value, row.MetricValues[0].Value);
              }
            }
            // [END analyticsdata_json_credentials_run_report_response]
        }
        static int Main(string[] args)
        {
            SampleRunReport();
            return 0;
        }
    }
}
// [END analyticsdata_json_credentials_quickstart]
