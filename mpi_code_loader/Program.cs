using Microsoft.Azure.Cosmos.Table;
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
            Console.WriteLine("Command line args:");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            var storage = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=mpicodes;AccountKey=TWtmmf57CbKKd0caGx/bvDu+nUr/mW/IIEW8aI/jjNap0JOFmAoT4hyPjBVxH/29DeA1l9gg44SCSO6+PAUmig==;EndpointSuffix=core.windows.net");
            var tblclient = storage.CreateCloudTableClient(new TableClientConfiguration());
            var table = tblclient.GetTableReference("mpicodes");


            var reader = new StreamReader(File.OpenRead(@"C:\Users\crine\source\repos\mpi_code_loader\mpi_code_loader\Codes.csv"));

            var codes = new List<mPI_Code>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var code = new mPI_Code
                {
                    PartitionKey="codes",
                    RowKey = values[0],
                    Code = values[0],
                    URL = values[2]
                };
                TableOperation insertOperation = TableOperation.InsertOrMerge(code);
                TableResult result =  table.Execute(insertOperation);
                codes.Add(code);
            }

           
            Console.WriteLine("Hello World!");
        }
    }
}
