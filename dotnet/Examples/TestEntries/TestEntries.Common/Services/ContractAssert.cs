using Microsoft.AspNetCore.Mvc;
using TestEntries.Common.Contracts;
using Xunit;

namespace TestEntries.Common.Services
{
    public class ContractAssert : Assert
    {
        public void AssertSuccess<T>(PackageResponse<T> responsePackage) where T: new()
        {
            True(responsePackage.ResponseHeader.Errors == null);
            Equal(responsePackage.ResponseHeader.ResultStatus, true);
        }
    }
}
