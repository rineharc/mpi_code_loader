using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace mpi_code_loader
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var storage = CloudStorageAccount.Parse(Configuration["Connection_string"]);
            var tblclient = storage.CreateCloudTableClient(new TableClientConfiguration());
            var table = tblclient.GetTableReference("mpicodes");


            var reader = new StreamReader(File.OpenRead(args[0]));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var code = new MPI_Code
                {
                    PartitionKey="codes",
                    RowKey = values[0],
                    Code = values[0],
                    URL = values[2]
                };
                TableOperation insertOperation = TableOperation.InsertOrMerge(code);
                _ = table.Execute(insertOperation);
            }
        }
    }
}
