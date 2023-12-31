﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public partial class PropBDContext
    {
        private IPropBDContextProcedures _procedures;

        public virtual IPropBDContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new PropBDContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IPropBDContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mostarProductosResult>().HasNoKey().ToView(null);
        }
    }

    public partial class PropBDContextProcedures : IPropBDContextProcedures
    {
        private readonly PropBDContext _context;

        public PropBDContextProcedures(PropBDContext context)
        {
            _context = context;
        }

        public virtual async Task<List<mostarProductosResult>> mostarProductosAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<mostarProductosResult>("EXEC @returnValue = [dbo].[mostarProductos]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
