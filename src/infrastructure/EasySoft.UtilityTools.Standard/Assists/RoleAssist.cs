using EasySoft.UtilityTools.Standard.Competence;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// RoleAssist
/// </summary>
public static class RoleAssist
{
    /// <summary>
    /// MergeCompetenceCollection
    /// </summary>
    /// <param name="list"></param>
    /// <param name="ceList"></param>
    /// <param name="accessWayPersistenceListGetter"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<List<CompetenceEntity>> MergeCompetenceCollectionAsync<T>(
        List<T> list,
        IEnumerable<CompetenceEntity> ceList,
        Func<IEnumerable<CompetenceEntity>, Task<IEnumerable<IAccessWay>>> accessWayPersistenceListGetter
    ) where T : IRole
    {
        var listCompetenceEntity = ceList.Select(o => o.GetClone()).ToList();

        foreach (var role in list)
            if (role.WhetherSuper == (int)Whether.Yes)
            {
                listCompetenceEntity.Add(new CompetenceEntity
                {
                    Name = ConstCollection.SuperRoleName,
                    GuidTag = ConstCollection.SuperRoleGuidTag
                });
            }
            else
            {
                var l = await GetCompetenceEntityCollectionAsync(role, accessWayPersistenceListGetter);

                foreach (var c in l)
                {
                    var exist = false;
                    for (var index = 0; index < listCompetenceEntity.Count; index++)
                    {
                        var ce = listCompetenceEntity[index];

                        if (ce.GuidTag != c.GuidTag) continue;

                        exist = true;

                        ce += c;

                        listCompetenceEntity[index] = ce;
                    }

                    if (!exist) listCompetenceEntity.Add(c);
                }
            }

        return listCompetenceEntity;
    }

    /// <summary>
    /// GetCompetenceEntityCollection
    /// </summary>
    /// <param name="role"></param>
    /// <param name="accessWayPersistenceListGetter"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<IList<CompetenceEntity>> GetCompetenceEntityCollectionAsync<T>(
        T role,
        Func<IEnumerable<CompetenceEntity>, Task<IEnumerable<IAccessWay>>> accessWayPersistenceListGetter
    ) where T : IRole
    {
        IList<CompetenceEntity> result = new List<CompetenceEntity>();

        if (!string.IsNullOrWhiteSpace(role.Competence))
        {
            var list = role.Competence
                .Split(',')
                .ToList()
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .ToList();

            foreach (var item in list)
            {
                var c = new CompetenceEntity();

                if (item.Contains("|"))
                {
                    var cv = item
                        .Split('|')
                        .ToList()
                        .Where(o => !string.IsNullOrWhiteSpace(o))
                        .ToList();

                    switch (cv.Count)
                    {
                        case 2:
                            c.GuidTag = cv[0];
                            c.ExpansionSet = cv[1];
                            break;

                        case 1:
                            c.GuidTag = cv[0];
                            break;
                    }
                }
                else
                {
                    c.GuidTag = item;
                }

                result.Add(c);
            }

            if (result.Count > 0)
            {
                var listAccessWay = new List<IAccessWay>();

                if (list.Count > 0)
                    listAccessWay = (await accessWayPersistenceListGetter(result)).ToList();

                foreach (var c in result)
                foreach (var aw in listAccessWay)
                {
                    if (aw.GuidTag != c.GuidTag) continue;

                    c.Name = aw.Name;
                    c.Explain = aw.Expand;
                    c.RelativePath = aw.RelativePath;
                }
            }
        }

        result = result.SortByPropertyValue(
            SortRule.Asc,
            ReflectionAssist.GetPropertyName<CompetenceEntity>(o => o.Name)
        ).ToList();

        return result;
    }
}