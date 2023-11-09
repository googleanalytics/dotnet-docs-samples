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

/* Google Analytics Admin API sample quickstart application.

Example usage:
    dotnet restore
    dotnet run

This application demonstrates the usage of the Analytics Admin API using
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

// [START analyticsadmin_quickstart]

using Google.Analytics.Admin.V1Beta;
using System;

namespace AnalyticsSamples
{
    class QuickStart
    {
        static void Main(string[] args)
        {
            var client = AnalyticsAdminServiceClient.Create();
            var response = client.ListAccounts( new ListAccountsRequest() );
            foreach( Account account in response )
            {
                Console.WriteLine("Account name: {0}", account.Name);
                Console.WriteLine("Display name: {0}", account.DisplayName);
                Console.WriteLine("Region code: {0}", account.RegionCode);
                Console.WriteLine("Update time: {0}", account.UpdateTime);
                Console.WriteLine("Create time: {0}", account.CreateTime);
                Console.WriteLine();
            }
        }
    }
}
// [END analyticsadmin_quickstart]