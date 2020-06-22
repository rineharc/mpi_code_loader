using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace mpi_code_loader
{
    class MPI_Code : TableEntity
    {
        public MPI_Code() { }

        public MPI_Code(string code) {
            PartitionKey = "codes";
            RowKey = code;
        }
        public string Code { get; set; }
        public string URL { get; set; }
    }
}
