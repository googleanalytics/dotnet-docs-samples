﻿// Copyright(c) 2020 Google Inc.
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

// [START analyticsdata_quickstart]
using Google.Analytics.Data.V1Beta;
using System;

namespace AnalyticsSamples
{
    class QuickStart
    {
        static void SampleRunReport(string propertyId="YOUR-GA4-PROPERTY-ID")
        {
            /**
             * TODO(developer): Uncomment this variable and replace with your
             *  Google Analytics 4 property ID before running the sample.
             */
            // propertyId = "YOUR-GA4-PROPERTY-ID";

            // [START analyticsdata_initialize]
            // Using a default constructor instructs the client to use the credentials
            // specified in GOOGLE_APPLICATION_CREDENTIALS environment variable.
            BetaAnalyticsDataClient client = BetaAnalyticsDataClient.Create();
            // [END analyticsdata_initialize]

            // [START analyticsdata_run_report]
            // Initialize request argument(s)
            RunReportRequest request = new RunReportRequest
            {
                Property = "properties/" + propertyId,
                Dimensions = { new Dimension{ Name="city"}, },
                Metrics = { new Metric{ Name="activeUsers"}, },
                DateRanges = { new DateRange{ StartDate="2020-03-31", EndDate="today"}, },
            };

            // Make the request
            var response = client.RunReport(request);
            // [END analyticsdata_run_report]

            // [START analyticsdata_run_report_response]
            Console.WriteLine("Report result:");
            foreach(Row row in response.Rows)
            {
                Console.WriteLine("{0}, {1}", row.DimensionValues[0].Value, row.MetricValues[0].Value);
            }
            // [END analyticsdata_run_report_response]
        }
        static int Main(string[] args)
        {
            SampleRunReport();
            return 0;
        }
    }
}
// [END analyticsdata_quickstart]
