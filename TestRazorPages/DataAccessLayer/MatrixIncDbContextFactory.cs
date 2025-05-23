using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer
{
    internal class MatrixIncDbContextFactory : IDesignTimeDbContextFactory<MatrixIncDbContext>
    {
        public MatrixIncDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MatrixIncDbContext>();
            optionsBuilder.UseSqlite("Data Source=matrixinc.db");

            return new MatrixIncDbContext(optionsBuilder.Options);
        }
    }
}
