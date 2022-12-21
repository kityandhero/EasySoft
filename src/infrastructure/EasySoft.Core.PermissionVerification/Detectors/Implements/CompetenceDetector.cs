using EasySoft.Core.PermissionVerification.Detectors.Interfaces;

namespace EasySoft.Core.PermissionVerification.Detectors.Implements;

/// <inheritdoc />
public class CompetenceDetector : ICompetenceDetector
{
    /// <inheritdoc />
    public Task<IList<CompetenceEntity>> GetCompetenceEntityCollection(string guidTag)
    {
        throw new NotImplementedException();
    }
}