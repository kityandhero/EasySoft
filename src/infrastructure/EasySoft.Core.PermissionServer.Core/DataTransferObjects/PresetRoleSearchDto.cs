using EasySoft.Core.PermissionServer.Core.Entities.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.DataTransferObjects;

/// <summary>
/// PresetRoleSearchDto
/// </summary>
public class PresetRoleSearchDto : PageSearchParams, IRoleEntity
{
    /// <summary>
    /// PresetRoleId
    /// </summary>
    public long PresetRoleId { get; set; }

    /// <inheritdoc />
    public string Competence { get; set; } = "";

    /// <inheritdoc />
    public int WhetherSuper { get; set; }

    /// <inheritdoc />
    public string Name { get; set; } = "";

    /// <inheritdoc />
    public string Description { get; set; } = "";

    /// <inheritdoc />
    public string Content { get; set; } = "";

    /// <inheritdoc />
    public int ModuleCount { get; set; }
}